using log4net;
using SimpleInjector;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using Task_ASP.BL.DTO;
using Task_ASP.DAL;


namespace Task_ASP.BL
{
    public class AggregatedCalculations : IAggregatedCalculations
    {
        internal static readonly Container _container;
        internal static readonly DBContext _context = new DBContext();
        internal static readonly ILog _logger =
            LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        static AggregatedCalculations()
        {
            _container = new Container();
            _container.RegisterSingleton<ILog>(() => _logger);
            _container.RegisterSingleton<DBContext>(() => _context);
            _container.Verify();
        }

        public decimal TotalCostForOrder(int OrderID)
        {
            _logger.Info(string.Format("TotalCostForOrder, OrderID = {0}", OrderID));
            return (from od in _context.OrderDetails
                    where od.OrderID == OrderID
                    select od)
                    .Sum(od => od.Product.Price * od.ProductQuantity);
        }

        public List<DTO_OrderCost> RecentOrdersForClient(int ClientID, int ordersNum = ConstForAggergatedCalculations.DefaultNumberOfOrders)
        {
            _logger.Info(string.Format("RecentOrdersForClient, ClientID = {0}, Number of orders =", ClientID, ordersNum));

            var orderList = (from o in _context.Orders
                             where o.ClientID == ClientID
                             orderby o.DateCreated descending, o.ID
                             select o)
                             .Take(ordersNum);

            return (from o in orderList
                    select
                    new DTO_OrderCost()
                    {
                        OrderID = o.ID,
                        Price = o.OrderDetails.Sum(od => od.Product.Price * od.ProductQuantity)
                    }
                    ).ToList();
        }
        public List<DTO_OrderCost> RecentOrdersForClient_Include(int ClientID, int ordersNum = ConstForAggergatedCalculations.DefaultNumberOfOrders)
        {
            _logger.Info(string.Format("RecentOrdersForClient_Include, ClientID = {0}, Number of orders =", ClientID, ordersNum));

            var orderList = (from o in _context.Orders
                             where o.ClientID == ClientID
                             orderby o.DateCreated descending, o.ID
                             select o)
                             .Take(ordersNum)
                             .Include(o => o.OrderDetails.Select(od => od.Product))
                             .ToList();

            var result = new List<DTO_OrderCost>();
            foreach (var o in orderList)
            {
                var totPrice = o.OrderDetails.Sum(od => od.Product.Price * od.ProductQuantity);
                result.Add(new DTO_OrderCost()
                {
                    OrderID = o.ID,
                    Price = totPrice
                });
            }
            return result;
        }

        public List<DTO_ClientOrderCost> ClientsTotalOrdersCostList()
        {
            _logger.Info("ClientsOrdersCost() via LINQ");

            return (from cl in _context.Clients
                    select
                    new DTO_ClientOrderCost
                    {
                        Name = cl.Name,
                        OrderCost = cl.Orders.Sum(o => o.OrderDetails.Sum(od => od.Product.Price * od.ProductQuantity))
                    }
                   ).ToList();
        }

        public List<DTO_ClientOrderCost> ClientsTotalOrdersCostList_StoredProcedure()
        {
            _logger.Info("ClientsOrdersCost_StoredProcedure()");
            return _context.Database.SqlQuery<DTO_ClientOrderCost>("exec ClientOrdersCost").ToList();
        }

    }
}