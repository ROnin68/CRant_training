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
using log4net;

namespace Task2
{
    [AttributeUsage(AttributeTargets.Field, AllowMultiple = false)]
    public class UseForEqualityCheck: System.Attribute
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
        [UseForEqualityCheck(1)]
        private string _Code;
        private string _Name;
        [UseForEqualityCheck(2)]
        private double _Price;

        public int ID {
            get {
                return _ID;
            }
            set {
                _ID = value;
            }
        }

        public string Code { 
            get {
                return _Code;
            }
            set {
                _Code = value;
            }
        }

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


    interface IEqualityComparer<T>
    {
        bool IsEquals(T obj);
    }

    public class ComparableProduct : Product, IComparable, IEqualityComparer<Product>
    {
        private System.Attribute[] thisAttributes;
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
        public bool IsEquals(Product otherProduct)
        {
            if (otherProduct == null) return false;

            //Lazy computation 
            if (thisType == null)
                thisType = typeof(ComparableProduct);

            bool result = false;

            foreach (PropertyInfo property in thisType.GetProperties())
            {
                if (property.GetCustomAttribute(typeof(UseForEqualityCheck)) != null)
                {
                    result = property.GetValue(this) == property.GetValue(otherProduct);
                    if (result == false)
                        return result;
                }
            }

            return result;
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
            List<ComparableProduct> uniqueProducts = new List<ComparableProduct>(numberOfProducts);

            foreach (ComparableProduct p in _Products) {
                bool alreadyAdded = false;

                int i = p.ID;

                foreach (ComparableProduct up in uniqueProducts) {
                    int j = up.ID;
                    alreadyAdded = p.IsEquals(up);
                }
                if (alreadyAdded) continue;
                uniqueProducts.Add(p);
            }

            Console.WriteLine("----------------------------------------------\n");
            Console.WriteLine("The List of UNIQUE products\n");
            foreach (ComparableProduct cp in uniqueProducts) {
                Console.WriteLine("Code: " + cp.Code + "\t" + "ID: " + cp.ID + "\t" + "Name: " + cp.Name + "\t" + "Price:" + cp.Price);
            }
            Console.ReadKey();
        }

        static void Main()
        {
            InitializeProductList();

            //LogToConsole();

            //LogToFile();

            ShowUniqueProducts();
        }
    }
}
