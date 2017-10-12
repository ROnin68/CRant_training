using System;
using Task5.Library.BusinessLogic;

namespace Task5
{
    class Program
    {
        static void Main()
        {
            var controller = new Task5Controller();

            controller.CreateAndInitializeDB();

            controller.CalculateOrdersCost();

            Console.WriteLine("DONE !");
            Console.ReadKey();
        }
    }
}
