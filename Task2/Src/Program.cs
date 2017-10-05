/*
 "Delphi --> C#" retraining program.

 TASK #2

 Programmer: Oleg Rokach

*/


using System;
using System.Collections.Generic;
using System.Threading;
using System.Linq;
using Entities;
using BusinessLogic;

namespace TrainingTask2
{
    class ProgramTask2
    {
        private static Task2Controller Controller;

        static ProgramTask2()
        {
            Controller = new Task2Controller();
            Controller.logMode = Task2Controller.LogMode.lmConsole;
        }

        static void Main()
        {

            List<Product> Products = Controller.CreateProductList();

            Controller.logString("The List of products - Unsorted\n", Task2Controller.LogLevel.llInfo);

            Controller.ShowProductList(Products);

            foreach (Product cp in Products)
                Controller.logString(string.Format("Code: {0} \t ID: {1} \t Name: {2} \t Price: {3}",
                                                 cp.Code, cp.ID, cp.Name, cp.Price), Task2Controller.LogLevel.llInfo);
            Thread.Sleep(TimeSpan.FromSeconds(Task1Controller.secToWait));

            Products.Sort();

            Controller.logString("\n----------------------------------------------\n", Task2Controller.LogLevel.llInfo);
            Controller.logString("The List of products - Sorted\n", Task2Controller.LogLevel.llInfo);
            Controller.ShowProductList(Products);
            Thread.Sleep(TimeSpan.FromSeconds(Task1Controller.secToWait));

            IEnumerable<Product> uniqueProducts = Products.Distinct(new ProductsComparer());

            Controller.logString("\n----------------------------------------------\n", Task2Controller.LogLevel.llInfo);
            Controller.logString("The List of UNIQUE products\n", Task2Controller.LogLevel.llInfo);

            foreach (Product cp in uniqueProducts)
            {
                Controller.logString(string.Format("Code: {0} \t ID: {1} \t Name: {2} \t Price: {3}",
                                                   cp.Code, cp.ID, cp.Name, cp.Price), Task2Controller.LogLevel.llInfo);
            }
            Console.ReadKey();
        }
    }
}
