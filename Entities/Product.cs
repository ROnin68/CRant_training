using System;
using System.ComponentModel.DataAnnotations;

namespace Entities
{
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = true)]
    public class UseForEqualityCheck : Attribute { }

    public class Product : IComparable
    {
        [Key]
        public int ID { get; set; }

        [UseForEqualityCheck]
        public string Code { get; set; }

        [UseForEqualityCheck]
        public string Name { get; set; }

        public decimal Price { get; set; }

        // Task3-specific: a new property to check migrations
        [UseForEqualityCheck]
        public int GroupID { get; set; }

        public int CompareTo(object obj)
        {
            if (obj == null) return 1;

            var otherProduct = obj as Product;
            if (otherProduct != null)
                return Code.CompareTo(otherProduct.Code);
            else
                throw new ArgumentException("The compared object is not a ComparableProduct");
        }
    }
}