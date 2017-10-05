/*
 "Delphi --> C#" retraining program.

 TASK #1

 Programmer: Oleg Rokach
*/


using System;
using System.Threading;
using System.Collections.Generic;
using Entities;
using BusinessLogic;


namespace TrainingTask1
{
    class ProgramTask1
    {
        private static Task1Controller Controller;

        static ProgramTask1()
        {
            Controller = new Task1Controller();
        }

        static void Main()
        {
            List <Product> Products = Controller.CreateProductList();

            Console.WriteLine("The List of products - Unsorted\n");

            foreach (Product cp in Products)
                Console.WriteLine(string.Format("Code: {0} \t ID: {1} \t Name: {2} \t Price: {3}", 
                                                 cp.Code, cp.ID, cp.Name, cp.Price));
            Thread.Sleep(TimeSpan.FromSeconds(Task1Controller.secToWait));

            Products.Sort();

            Console.WriteLine("\n----------------------------------------------\n");
            Console.WriteLine("The List of products - Sorted\n");

            foreach (Product cp in Products)
                Console.WriteLine(string.Format("Code: {0} \t ID: {1} \t Name: {2} \t Price: {3}",
                                                 cp.Code, cp.ID, cp.Name, cp.Price));
            Thread.Sleep(TimeSpan.FromSeconds(Task1Controller.secToWait));
            Console.WriteLine("Press Any Key...");
            Console.ReadKey();
        }
    }
}
