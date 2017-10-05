using DB;
using Entities;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Linq;



namespace BusinessLogic
{
    public class Task4Controller : Task3Controller
    {
        public static readonly Stopwatch stopWatch = new Stopwatch();

        internal DBContext _DBContext;
        internal Random random = new Random();
        internal static DateTime firstOrderDate = new DateTime(2010, 1, 1);
        internal static int ordersDateRange = (DateTime.Now - firstOrderDate).Days;


        public Task4Controller()
            : base()
        {
            _DBContext = new DBContext();
        }

        internal DateTime getRandomDate()
        {
            return firstOrderDate.AddDays(random.Next(ordersDateRange));
        }

        internal int getRandomFromArray(int[] arr)
        {
            return arr[random.Next(0, arr.Length)];
        }


        internal List<Client> CreateClientList()
        {
            return new List<Client>
            {
                new Client { ID = 1,  Name = "Alfreds Futterkiste" },
                new Client { ID = 2,  Name = "Blauer See Delikatessen" },
                new Client { ID = 3,  Name = "Centro comercial Moctezuma" },
                new Client { ID = 4,  Name = "Du monde entier"},
                new Client { ID = 5,  Name = "Folies gourmandes"},
                new Client { ID = 6,  Name = "Great Lakes Food Market"},
                new Client { ID = 7,  Name = "Hungry Coyote Import Store"},
                new Client { ID = 8,  Name = "Königlich Essen"},
                new Client { ID = 9,  Name = "Lazy K Kountry Store"},
                new Client { ID = 10, Name = "Polski Zajazd"},
                new Client { ID = 11, Name = "Rancho grande"},
                new Client { ID = 12, Name = "Seven Seas Imports"},
                new Client { ID = 13, Name = "Toms Spezialitäten"},
                new Client { ID = 14, Name = "Vins et alcools Chevalier"}
            };
        }

        internal List<Order> CreateOrderList(int orderCount = 500)
        {
            var orders = new List<Order>();
            int[] clientIDs = _DBContext.Clients.Select(c => c.ID).ToArray();

            for (int i = 0; i < orderCount; i++)
            {
                orders.Add(
                    new Order
                    {
                        ClientID = getRandomFromArray(clientIDs),
                        DateCreated = getRandomDate(),
                        Status = (OrderStatus)random.Next((int)OrderStatus.osPending, (int)OrderStatus.osCompleted),
                    });
            }
            return orders;
        }
        internal List<OrderDetail> CreateOrderDetailList(int detailCount = 15)
        {
            var ordDetails = new List<OrderDetail>();

            int[] clientIDs = _DBContext.Clients.Select(c => c.ID).ToArray();
            int[] orderIDs = _DBContext.Orders.Select(o => o.ID).ToArray();

            for (int io = 0; io < orderIDs.Length; io++)
            {
                for (int id = 0; id < detailCount; id++)
                {
                    ordDetails.Add(new OrderDetail
                    {
                        OrderID = io,
                        ProductID = getRandomFromArray(clientIDs),
                        ProductQuantity = random.Next(10)
                    });
                }
            }
            return ordDetails;
        }


        public void CreateProductsAndClients()
        {
            if (_DBContext.Products.Count() == 0)
            {
                logString("Creating Products table...", LogLevel.llInfo);
                _DBContext.Products.AddRange(CreateProductList());
                _DBContext.SaveChanges();
            }

            if (_DBContext.Clients.Count() == 0)
            {
                logString("Creating Products table...", LogLevel.llInfo);
                _DBContext.Clients.AddRange(CreateClientList());
                _DBContext.SaveChanges();
            }
        }

        public void DeleteOrdersIfExists()
        {
            var changed = false;
            if (_DBContext.Orders.Count() > 0)
            {
                _DBContext.Orders.RemoveRange(_DBContext.Orders);
                changed = true;
            };


            if (_DBContext.Orders.Count() > 0)
            {
                _DBContext.OrderDetails.RemoveRange(_DBContext.OrderDetails);
                changed = true;
            };

            if (changed) _DBContext.SaveChanges();
        }

        public void AddOrdersAutoDetectChangesON()
        {
            logString("Add Orders with AutoDetectChanges = ON...", LogLevel.llInfo);
            var orders = CreateOrderList();

            stopWatch.Reset();
            stopWatch.Start();

            foreach (Order ord in orders)
                _DBContext.Orders.Add(ord);
            _DBContext.SaveChanges();

            stopWatch.Stop();

            logString(string.Format("Insertion of {0} orders with AutoDetectChanges = ON took {1} ms",
                                     orders.Count(), stopWatch.ElapsedMilliseconds), LogLevel.llInfo);

        }

        public void AddOrdersAutoDetectChangesOFF()
        {
            logString("Add Orders with AutoDetectChanges = OFF...", LogLevel.llInfo);
            var orders = CreateOrderList();

            stopWatch.Reset();
            stopWatch.Start();

            try
            {
                _DBContext.Configuration.AutoDetectChangesEnabled = false;
                foreach (Order ord in orders)
                    _DBContext.Orders.Add(ord);

                _DBContext.SaveChanges();
            }
            finally
            {
                _DBContext.Configuration.AutoDetectChangesEnabled = true;
            }

            stopWatch.Stop();
            logString(string.Format("Insertion of {0} orders with AutoDetectChanges = OFF has been took {1} ms",
                                     orders.Count(), stopWatch.ElapsedMilliseconds), LogLevel.llInfo);
        }

        public void AddOrderDetails()
        {
            logString("Adding OrderDetails using SQLBulkCopy...", LogLevel.llInfo);

            var orderDetails = CreateOrderDetailList();

            DataTable orderDetailsTable = new DataTable();
            orderDetailsTable.Columns.Add("ID");
            orderDetailsTable.Columns.Add("OrderID");
            orderDetailsTable.Columns.Add("ProductID");
            orderDetailsTable.Columns.Add("ProductQuantity");

            var connString = ConfigurationManager.ConnectionStrings["DBConnection"].ToString();

            stopWatch.Reset();
            stopWatch.Start();

            foreach (OrderDetail od in orderDetails)
            {
                orderDetailsTable.Rows.Add(null, od.OrderID, od.ProductID, od.ProductQuantity);
            }

            using (SqlBulkCopy bulkCopy = new SqlBulkCopy(connString))
            {
                bulkCopy.DestinationTableName = "dbo.OrderDetails";
                bulkCopy.WriteToServer(orderDetailsTable);
            }

            stopWatch.Stop();
            logString(string.Format("Insertion of 15 details for each order has been took {0} ms", stopWatch.ElapsedMilliseconds), LogLevel.llInfo);
        }
    }
}