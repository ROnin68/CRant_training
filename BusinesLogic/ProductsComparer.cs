using System;
using System.Collections.Generic;
using Entities;

namespace BusinessLogic
{
    public class ProductsComparer : IEqualityComparer<Product>
    {
        public bool Equals(Product x, Product y)
        {
            if (ReferenceEquals(x, y)) return true;

            if (ReferenceEquals(x, null) || ReferenceEquals(y, null)) return false;

            var is_equal = true;
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
            if (ReferenceEquals(product, null)) return 0;

            var hashCode = 0;
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
}