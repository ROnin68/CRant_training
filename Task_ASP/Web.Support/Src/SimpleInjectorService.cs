using SimpleInjector;
using Task_ASP.AppFacade;
using Task_ASP.BL;
using Task_ASP.DAL;

namespace Task_ASP.Web.Support
{
    public static class SimpleInjectorService
    {
        public static void RegisterServices(this Container container)
        {

            container.RegisterSingleton<log4net.ILog>(() =>
                log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType)
            );
            container.RegisterSingleton<DBContext>();
            container.Register<IAggregatedCalculations, AggregatedCalculations>();
            container.Register<IClientManager, ClientManager>();
            container.Register<IClientFacade, ClientFacade>();
        }
    }
}
