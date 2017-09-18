/*
 "Delphi --> C#" retraining program.

 TASK #1

 Programmer: Oleg Rokach

 Completed: 18.09.2017
*/


using System;
using System.Collections.Generic;

namespace Task1
{
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


    public class ComparableProduct: Product, IComparable
    {
        public int CompareTo(object obj) {
            if (obj == null) return 1;

            ComparableProduct otherProduct = obj as ComparableProduct;
            if (otherProduct != null)
                return this.Code.CompareTo(otherProduct.Code);
            else
                throw new ArgumentException("The compared object is not a ComparableProduct");
        }
    }

    class ProgramTask1
    {
        static void Main()
        {

            List <ComparableProduct> products = new List<ComparableProduct>(5);

            products.Add(new ComparableProduct() { ID = 1, Code = "I5_760_CPU", Name = "Intel Core i5-760", Price = 120 });
            products.Add(new ComparableProduct() { ID = 2, Code = "I5_760S_CPU", Name = "Intel Core i5-760S", Price = 115 });
            products.Add(new ComparableProduct() { ID = 3, Code = "I5_750_CPU", Name = "Intel Core i5-750", Price = 100 });
            products.Add(new ComparableProduct() { ID = 4, Code = "I7-8650U_CPU", Name = "Intel Core i7-8650U", Price = 450 });
            products.Add(new ComparableProduct() { ID = 5, Code = "I7_7820HK_CPU", Name = "Intel Core i7-7820HK", Price = 300 });

            Console.WriteLine("The List of products - Unsorted\n");

            foreach (ComparableProduct cp in products)
                Console.WriteLine("Code: " + cp.Code + "\t" + "ID: " + cp.ID + "\t" + "Name: " + cp.Name + "\t" + "Price:" + cp.Price);

            products.Sort();

            Console.WriteLine("----------------------------------------------\n");
            Console.WriteLine("The List of products - Sorted\n");

            foreach (ComparableProduct cp in products)
                Console.WriteLine("Code: " + cp.Code + "\t" + "ID: " + cp.ID + "\t" + "Name: " + cp.Name + "\t" + "Price:" + cp.Price);

            Console.ReadKey();
        }
    }
}
