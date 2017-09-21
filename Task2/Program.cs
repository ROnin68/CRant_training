/*
 "Delphi --> C#" retraining program.

 TASK #2

 Programmer: Oleg Rokach

 Completed: 20.09.2017
*/


using System;
using System.Collections.Generic;
using System.Threading;
using System.Reflection;
using System.Linq;
using log4net;

namespace Task2
{
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = true)]
    public class UseForEqualityCheck : Attribute
    {
        private int _propertyID;

        public UseForEqualityCheck(int propertyID)
        {
            _propertyID = propertyID;
        }

        public int PropertyID  {
            get {
                return _propertyID;
            }
        }
    }

    public class Product
    {
        private int _ID;
        private string _Code;
        private string _Name;
        private double _Price;

        public int ID {
            get {
                return _ID;
            }
            set {
                _ID = value;
            }
        }

        [UseForEqualityCheck(1)]
        public string Code { 
            get {
                return _Code;
            }
            set {
                _Code = value;
            }
        }

        [UseForEqualityCheck(2)]
        public string Name {
            get {
                return _Name;
            }
            set {
                _Name = value;
            }
        }

        public double Price {
            get {
                return _Price;
            }
            set {
                _Price = value;
            }
        }
    }

    public class ComparableProduct : Product, IComparable
    {
        private System.Type thisType;
        public int CompareTo(object obj)
        {
            if (obj == null) return 1;

            ComparableProduct otherProduct = obj as ComparableProduct;
            if (otherProduct != null)
                return this.Code.CompareTo(otherProduct.Code);
            else
                throw new ArgumentException("The compared object is not a ComparableProduct");
        }
    }

    public class ProductComparer : IEqualityComparer<Product>
    {
        public bool Equals(Product x, Product y)
        {
            if (ReferenceEquals(x, y)) return true;

            if (ReferenceEquals(x, null) || ReferenceEquals(y, null)) return false;

            bool is_equal = true;
            foreach (var property in typeof(Product).GetProperties())
            {
                var attribute = Attribute.GetCustomAttribute(property, typeof(UseForEqualityCheck)) as UseForEqualityCheck;
                if (attribute != null)
                {
                    is_equal = is_equal && property.GetValue(x).Equals(property.GetValue(y));
                    if (!is_equal) break;
                }
            }
            return is_equal;
        }

        public int GetHashCode(Product product)
        {
            if (Object.ReferenceEquals(product, null)) return 0;

            int hashCode = 0;
            foreach (var property in typeof(Product).GetProperties())
            {
                var attribute = Attribute.GetCustomAttribute(property, typeof(UseForEqualityCheck)) as UseForEqualityCheck;
                if (attribute != null)
                {
                    hashCode = hashCode ^ property.GetValue(product).GetHashCode();
                }
            }
            return hashCode;
        }
    }


    class ProgramTask2
    {
        private static int secToWait = 2;
        private static int numberOfProducts = 6;

        //Declare an instance for log4net
        private static readonly ILog _Logger = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        private static List<ComparableProduct> _Products;

        private static void LogToConsole()
        {
            if (_Products == null) {
                Console.Beep();
                Console.WriteLine("Error: The _Products variable not initialized!");
                Thread.Sleep(TimeSpan.FromSeconds(secToWait));
                return;
            }

            Console.WriteLine("The List of products - Unsorted\n");

            foreach (ComparableProduct cp in _Products) {
                Console.WriteLine("Code: " + cp.Code + "\t" + "ID: " + cp.ID + "\t" + "Name: " + cp.Name + "\t" + "Price:" + cp.Price);
                Thread.Sleep(TimeSpan.FromSeconds(secToWait));
            } 

            _Products.Sort();

            Console.WriteLine("----------------------------------------------\n");
            Console.WriteLine("The List of products - Sorted\n");

            foreach (ComparableProduct cp in _Products) {
                Console.WriteLine("Code: " + cp.Code + "\t" + "ID: " + cp.ID + "\t" + "Name: " + cp.Name + "\t" + "Price:" + cp.Price);
                Thread.Sleep(TimeSpan.FromSeconds(secToWait));
            }

            Console.ReadKey();
        }

        private static void LogToFile()
        {
            if (_Products == null)
            {
                _Logger.Fatal("Error: The _Products variable not initialized!");
                Thread.Sleep(TimeSpan.FromSeconds(secToWait));
                return;
            }

            _Logger.Info("The List of products - Unsorted\n");

            foreach (ComparableProduct cp in _Products)
            {
                _Logger.Info("Code: " + cp.Code + "\t" + "ID: " + cp.ID + "\t" + "Name: " + cp.Name + "\t" + "Price:" + cp.Price);
                Thread.Sleep(TimeSpan.FromSeconds(secToWait));
            }

            _Products.Sort();

            _Logger.Info("----------------------------------------------\n");
            _Logger.Info("The List of products - Sorted\n");

            foreach (ComparableProduct cp in _Products)
            {
                _Logger.Info("Code: " + cp.Code + "\t" + "ID: " + cp.ID + "\t" + "Name: " + cp.Name + "\t" + "Price:" + cp.Price);
                Thread.Sleep(TimeSpan.FromSeconds(secToWait));
            }

            Console.WriteLine("DONE !");
            Console.ReadKey();
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
            IEnumerable<Product> uniqueProducts = _Products.Distinct(new ProductComparer());

            Console.WriteLine("----------------------------------------------\n");
            Console.WriteLine("The List of UNIQUE products\n");
            foreach (Product cp in uniqueProducts) {
                Console.WriteLine("Code: " + cp.Code + "\t" + "ID: " + cp.ID + "\t" + "Name: " + cp.Name + "\t" + "Price:" + cp.Price);
            }
            Console.ReadKey();
        }

        static void Main()
        {
            InitializeProductList();

            LogToConsole();

            LogToFile();

            ShowUniqueProducts();
        }
    }
}
