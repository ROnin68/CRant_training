/*
 "Delphi --> C#" retraining program.

 TASK #3

 Programmer: Oleg Rokach

 Completed: 25.09.2017
*/


using System;
using System.Collections.Generic;
using System.Threading;
using System.Linq;
using log4net;

namespace Task3
{
    class ProgramTask3
    {
        public enum LogLevel { llFatal, llError, llWarn, llInfo, llDebug };

        public delegate void LogString(string logMessage, LogLevel logLevel);

        //private
        private const int secToWait = 1;
        private const int numberOfProducts = 6;

        //Declare an instance for log4net
        private static readonly ILog _Logger = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        private static List<ComparableProduct> _Products;
        private static ProductDBContext _ProductsDB;

        private static List<ComparableProduct> CreateProductList()
        {
            logString(string.Format("Create a new product list containing {0} products.", numberOfProducts), LogLevel.llInfo);
            return new List<ComparableProduct>(numberOfProducts)
            {
                new ComparableProduct() { ID = 1, Code = "I5_760_CPU", Name = "Intel Core i5-760", Price = 120, GroupID = 1 },
                new ComparableProduct() { ID = 2, Code = "I5_760S_CPU", Name = "Intel Core i5-760S", Price = 115, GroupID = 1 },
                new ComparableProduct() { ID = 3, Code = "I5_750_CPU", Name = "Intel Core i5-750", Price = 100, GroupID = 1 },
                new ComparableProduct() { ID = 4, Code = "I7-8650U_CPU", Name = "Intel Core i7-8650U", Price = 450, GroupID = 2 },
                new ComparableProduct() { ID = 5, Code = "I7_7820HK_CPU", Name = "Intel Core i7-7820HK", Price = 300, GroupID = 2 },
                new ComparableProduct() { ID = 6, Code = "I5_760_CPU", Name = "Intel Core i5-760", Price = 125, GroupID = 1 }  // Dublicate for the 1th item
            };
        }


    //public
        public static LogString logString { get; set; }

        public static void LogToConsole(string logMessage, LogLevel logLevel)
        {
            if (logLevel == LogLevel.llFatal || logLevel == LogLevel.llError)
                Console.Beep();
            Console.WriteLine(logMessage);
            Thread.Sleep(TimeSpan.FromSeconds(secToWait));
        }

        public static void LogToFileNet(string logMessage, LogLevel logLevel)
        {
            switch (logLevel)
            {
                case LogLevel.llFatal:
                    _Logger.Fatal(logMessage);
                    break;

                case LogLevel.llError:
                    _Logger.Error(logMessage);
                    break;

                case LogLevel.llWarn:
                    _Logger.Warn(logMessage);
                    break;

                case LogLevel.llInfo:
                    _Logger.Info(logMessage);
                    break;

                case LogLevel.llDebug:
                    _Logger.Debug(logMessage);
                    break;
            }
            Thread.Sleep(TimeSpan.FromSeconds(secToWait));
        }

        private static void ShowProductList(List<ComparableProduct> productList)

        {
            if (productList == null)
            {
                throw new ArgumentException("FATAL: Undefined argument!");
            }

            if (logString == null)
            {
                throw new Exception("FATAL: Undefined log method!");
            }

            foreach (ComparableProduct cp in productList)
                logString(string.Format("Code: {0} \t ID: {1} \t Name: {2} \t Price: {3} \t GroupID {4} ", 
                                               cp.Code, cp.ID, cp.Name, cp.Price, cp.GroupID), LogLevel.llInfo);
        }

        private static void SortProductList()
        {
            if (logString == null)
            {
                throw new Exception("FATAL: Undefined log method!");
            }

            logString("The List of products - Unsorted\n", LogLevel.llInfo);

            ShowProductList(_Products);

            _Products.Sort();

            logString("----------------------------------------------\n", LogLevel.llInfo);
            logString("The List of products - Sorted\n", LogLevel.llInfo);

            ShowProductList(_Products);

        }

        private static void InitializeProducts()
        {
            logString("Create a new DB context.", LogLevel.llInfo);
            _ProductsDB = new ProductDBContext();

            if (_ProductsDB.Products.Count() == 0) {
                _Products = CreateProductList();
                _ProductsDB.Products.AddRange(_Products);
                _ProductsDB.SaveChanges();
            }
        }

        private static void ShowDBContext()
        {
            var query = from products in _ProductsDB.Products select products;
            logString("Show DB Content using query: \n", LogLevel.llInfo);
            ShowProductList(query.ToList());
            logString("---", LogLevel.llInfo);
        }

        private static void ShowUniqueProducts()
        {
            IEnumerable<Product> uniqueProducts = _Products.Distinct(new ProductsComparer());

            logString = LogToConsole;

            Console.WriteLine("----------------------------------------------\n");
            Console.WriteLine("The List of UNIQUE products\n");

            ShowProductList(_Products);

            Console.ReadKey();
        }

        static void Main()
        {

            logString = LogToConsole;
//            logString = LogToFileNet;

            InitializeProducts();

            ShowDBContext();

            SortProductList();

            ShowUniqueProducts();

            Console.WriteLine("DONE !");
            Console.ReadKey();
        }
    }
}
