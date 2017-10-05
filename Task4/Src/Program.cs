/*
 "Delphi --> C#" retraining program.

 TASK #4

 Programmer: Oleg Rokach
*/

using BusinessLogic;
using System;

namespace TrainingTask4
{
    class ProgramTask4
    {
        private static Task4Controller _controller;

        static ProgramTask4()
        {
            _controller = new Task4Controller();

            _controller.logMode = Task2Controller.LogMode.lmConsole;

        }

        static void Main()
        {
            _controller.CreateProductsAndClients();

            _controller.DeleteOrdersIfExists();

            _controller.AddOrdersAutoDetectChangesON();

            _controller.AddOrdersAutoDetectChangesOFF();

            _controller.AddOrderDetails();


            Console.WriteLine("DONE !");
            Console.ReadKey();
        }
    }
}
