using BusinessLogic;
using SimpleInjector;
using System;
using System.Diagnostics;
using System.Linq;
using Task5.Library.DB;

namespace Task5.Library.BusinessLogic
{
    public class Task5Controller
    {
        internal static readonly Container _container;
        internal static readonly Stopwatch _watch = new Stopwatch();
        internal static readonly AggregatedCalculations _calculator = new AggregatedCalculations();

        static Task5Controller()
        {
            _container = new Container();
            _container.RegisterSingleton<AggregatedCalculations>(() => _calculator);
            _container.RegisterSingleton<DBContext>();
            _container.Verify();
        }

        public void CreateAndInitializeDB()
        {
            var contr = new Task4Controller();
            contr.logMode = Task2Controller.LogMode.lmConsole;
            contr.MuteLog = true;
            contr.CreateProductsAndClients();
            contr.DeleteOrdersIfExists();
            contr.AddOrdersAutoDetectChangesON();
            contr.AddOrderDetails();
        }

        public void CalculateOrdersCost()
        {
            using (var db = _container.GetInstance<DBContext>())
            {
                // 1) - Total order's cost for specific ID
                var ordID = db.Orders.FirstOrDefault().ID;
                CalculateTotalCostForOrder(ordID);

                // 2) - Show 15 most recent orders and cost of each order for a given client ID 
                var clientID = db.Clients.FirstOrDefault().ID;
                ShowRecentOrdersForClient(clientID);
                ShowRecentOrdersForClient_Include(clientID);

                // 3) - For each client, get total cost of all their orders.
                ShowClientsTotalOrdersCost();
                ShowClientsTotalOrdersCost_StoredProcedure();
            }
        }

        private void CalculateTotalCostForOrder(int ordID)
        {
            _watch.Reset();
            _watch.Start();
            var totalCost = _calculator.TotalCostForOrder(ordID);
            _watch.Stop();

            Console.WriteLine("Total cost of the order #{0} = {1}; Elapsed time = {2} ms",
                               ordID, totalCost, _watch.ElapsedMilliseconds);
            Console.WriteLine("-----------------------------");
        }

        private void ShowRecentOrdersForClient(int clientID)
        {
            _watch.Reset();
            _watch.Start();
            var orderList = _calculator.RecentOrdersForClient(clientID);
            _watch.Stop();

            Console.WriteLine("15 most recent orders for clientID #{0}, Elapsed time = {1} ms",
                               clientID, _watch.ElapsedMilliseconds);
            foreach (var o in orderList)
            {
                Console.WriteLine(string.Format("   Order #{0}; Price = {1}", o.OrderID, o.Price));
            }
            Console.WriteLine("-----------------------------");
        }

        private void ShowRecentOrdersForClient_Include(int clientID)
        {
            _watch.Reset();
            _watch.Start();
            var orderList = _calculator.RecentOrdersForClient_Include(clientID);
            _watch.Stop();

            Console.WriteLine("15 most recent orders for clientID #{0} using INCLUDE, Elapsed time = {1} ms",
                               clientID, _watch.ElapsedMilliseconds);
            foreach (var o in orderList)
            {
                Console.WriteLine(string.Format("   Order #{0}; Price = {1}", o.OrderID, o.Price));
            }
            Console.WriteLine("-----------------------------");
        }

        private void ShowClientsTotalOrdersCost()
        {
            _watch.Reset();
            _watch.Start();
            var clientsOrdersCostList = _calculator.ClientsTotalOrdersCostList();
            _watch.Stop();

            Console.WriteLine("Total cost of client orders, Elapsed time = {0} ms", _watch.ElapsedMilliseconds);
            foreach (var c in clientsOrdersCostList)
            {
                Console.WriteLine(string.Format("   {0} = {1}", c.Name, c.OrderCost));
            }
            Console.WriteLine();
        }

        private void ShowClientsTotalOrdersCost_StoredProcedure()
        {
            _watch.Reset();
            _watch.Start();
            var clientsOrdersCostList = _calculator.ClientsTotalOrdersCostList();
            _watch.Stop();

            Console.WriteLine("Total cost of client orders (by SP), Elapsed time = {0} ms", _watch.ElapsedMilliseconds);
            foreach (var c in clientsOrdersCostList)
            {
                Console.WriteLine(string.Format("   {0} = {1}", c.Name, c.OrderCost));
            }
            Console.WriteLine();
        }
    }
}