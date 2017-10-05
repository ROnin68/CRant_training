using DB;
using System;

namespace BusinessLogic
{
    public class Task3Controller : Task2Controller
    {
        public Task3Controller()
        { ProductDB = null; }   //new ProductDBContext("DBConnection")

        public ProductDBContext ProductDB;

        public void UpdateGroupIDField()
        {
            if (ProductDB == null)
                throw new Exception("Uninitialized ProductsDB!");
            var p = ProductDB.Products.Find(1);
            if (p != null) p.GroupID = 1;

            p = ProductDB.Products.Find(2);
            if (p != null) p.GroupID = 1;

            p = ProductDB.Products.Find(3);
            if (p != null) p.GroupID = 1;

            p = ProductDB.Products.Find(4);
            if (p != null) p.GroupID = 1;

            p = ProductDB.Products.Find(5);
            if (p != null) p.GroupID = 2;

            p = ProductDB.Products.Find(6);
            if (p != null) p.GroupID = 2;

            p = ProductDB.Products.Find(7);
            if (p != null) p.GroupID = 2;

            p = ProductDB.Products.Find(8);
            if (p != null) p.GroupID = 3;

            p = ProductDB.Products.Find(9);
            if (p != null) p.GroupID = 3;

            p = ProductDB.Products.Find(10);
            if (p != null) p.GroupID = 3;

            p = ProductDB.Products.Find(11);
            if (p != null) p.GroupID = 2;

            ProductDB.SaveChanges();
        }
    }
}