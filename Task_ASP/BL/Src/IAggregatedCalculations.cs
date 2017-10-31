using System.Collections.Generic;
using Task_ASP.BL.DTO;

namespace Task_ASP.BL
{
    public static class ConstForAggergatedCalculations
    {
        public const int DefaultNumberOfOrders = 15;
    }

    public interface IAggregatedCalculations
    {
        decimal TotalCostForOrder(int OrderID);
        List<DTO_OrderCost> RecentOrdersForClient(int ClientID, int ordersNum = ConstForAggergatedCalculations.DefaultNumberOfOrders);
        List<DTO_OrderCost> RecentOrdersForClient_Include(int ClientID, int ordersNum = ConstForAggergatedCalculations.DefaultNumberOfOrders);
        List<DTO_ClientOrderCost> ClientsTotalOrdersCostList();
        List<DTO_ClientOrderCost> ClientsTotalOrdersCostList_StoredProcedure();
    }
}