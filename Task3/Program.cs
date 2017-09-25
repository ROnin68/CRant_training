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
        private const int secToWait = 2;
        private const int numberOfProducts = 6;

        //Declare an instance for log4net
        private static readonly ILog _Logger = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        private static List<ComparableProduct> _Products;

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
                    _Logger.Fatal(logMessage);
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
                logString(string.Format("Code: {0} \t ID: {1} \t Name: {2} \t Price: {3} ", cp.Code, cp.ID, cp.Name, cp.Price), LogLevel.llInfo);

        }

        private static void RunSort()
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

        private static void InitializeProductList()
        {

            _Products = new List<ComparableProduct>(numberOfProducts);

            _Products.Add(new ComparableProduct() { ID = 1, Code = "I5_760_CPU", Name = "Intel Core i5-760", Price = 120 });
            _Products.Add(new ComparableProduct() { ID = 2, Code = "I5_760S_CPU", Name = "Intel Core i5-760S", Price = 115 });
            _Products.Add(new ComparableProduct() { ID = 3, Code = "I5_750_CPU", Name = "Intel Core i5-750", Price = 100 });
            _Products.Add(new ComparableProduct() { ID = 4, Code = "I7-8650U_CPU", Name = "Intel Core i7-8650U", Price = 450 });
            _Products.Add(new ComparableProduct() { ID = 5, Code = "I7_7820HK_CPU", Name = "Intel Core i7-7820HK", Price = 300 });
            _Products.Add(new ComparableProduct() { ID = 6, Code = "I5_760_CPU", Name = "Intel Core i5-760", Price = 120 }); // Dublicate for 1th item!
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
            InitializeProductList();

            logString = LogToConsole;
//            logString = LogToFileNet;

            RunSort();

            ShowUniqueProducts();

            Console.WriteLine("DONE !");
            Console.ReadKey();
        }
    }
}
