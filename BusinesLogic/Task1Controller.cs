using Entities;
using System.Collections.Generic;

namespace BusinessLogic
{
    public class Task1Controller
    {
        public const int secToWait = 1;
        public const int numberOfProducts = 11;

        public List<Product> CreateProductList()
        {
            return new List<Product>(numberOfProducts)
            {
                #region "CreateProductList"
                new Product() { ID = 01, Code = "I3_330_CPU", Name = "Intel Core i3-760", Price = 85, GroupID = 1 },
                new Product() { ID = 02, Code = "I3_350_CPU", Name = "Intel Core i3-760", Price = 90, GroupID = 1 },
                new Product() { ID = 03, Code = "I3_370_CPU", Name = "Intel Core i3-760", Price = 95, GroupID = 1 },
                new Product() { ID = 04, Code = "I3_390_CPU", Name = "Intel Core i3-760", Price = 99, GroupID = 1 },
                new Product() { ID = 05, Code = "I5_760_CPU", Name = "Intel Core i5-760", Price = 120, GroupID = 2 },
                new Product() { ID = 06, Code = "I5_760S_CPU", Name = "Intel Core i5-760S", Price = 115, GroupID = 2 },
                new Product() { ID = 07, Code = "I5_750_CPU", Name = "Intel Core i5-750", Price = 100, GroupID = 2 },
                new Product() { ID = 08, Code = "I7-8650U_CPU", Name = "Intel Core i7-8650U", Price = 450, GroupID = 3 },
                new Product() { ID = 09, Code = "I7_5820K_CPU", Name = "Intel Core i7-5820K", Price = 350, GroupID = 3 },
                new Product() { ID = 10, Code = "I7_7820HK_CPU", Name = "Intel Core i7-7820HK", Price = 300, GroupID = 3 },
                new Product() { ID = 11, Code = "I5_760_CPU", Name = "Intel Core i5-760", Price = 125, GroupID = 2 }  // Dublicate for the 5th item
                #endregion
            };
        }

    }
}