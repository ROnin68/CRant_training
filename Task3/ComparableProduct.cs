using System;

namespace Task3
{
    public class ComparableProduct : Product, IComparable
    {
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
}