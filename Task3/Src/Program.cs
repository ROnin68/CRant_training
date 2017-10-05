/*
 "Delphi --> C#" retraining program.

 TASK #3

 Programmer: Oleg Rokach

*/

using BusinessLogic;
using DB;
using System;
using System.Linq;

namespace TrainingTask3
{
    class ProgramTask3
    {
        private static Task3Controller _controller;

        static ProgramTask3()
        {
            _controller = new Task3Controller();
            _controller.logMode = Task2Controller.LogMode.lmConsole;

            _controller.logString("Get DB context.", Task2Controller.LogLevel.llInfo);
            _controller.ProductDB = new ProductDBContext();

            if (_controller.ProductDB.Products.Count() == 0)
            {
                _controller.ProductDB.Products.AddRange(_controller.CreateProductList());
                _controller.ProductDB.SaveChanges();
            }
        }
        private static void ShowDBContext()
        {
            var query = from products in _controller.ProductDB.Products select products;
            _controller.logString("Show DB Content using query: \n", Task2Controller.LogLevel.llInfo);
            _controller.ShowProductList(query.ToList());
            _controller.logString("---", Task2Controller.LogLevel.llInfo);
        }

        static void Main()
        {
            ShowDBContext();

            _controller.UpdateGroupIDField();

            ShowDBContext();

            Console.WriteLine("DONE !");
            Console.ReadKey();
        }
    }
}
