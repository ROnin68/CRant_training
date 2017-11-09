using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Web;
using Task_ASP.AppFacade.DTO;
using Task_ASP.Web.Models.API;

namespace Task_ASP.Web.Mapping.API
{
    static public class ClientMapper
    {
        public static OrdersListModel ToOrderListModel(this List<DTO.DTO_Order> orderList, int firstID, int lastID)
        {
            var result = new OrdersListModel();

            result.Data = orderList;
            if (orderList.Count != 0)
            {
                var firstOrderID = orderList.First().ID;
                var lastOrderID = orderList.Last().ID;

                result.Paging.previous = (firstOrderID != firstID)
                        ? $"http://localapp/api/clients/2/orders?idlt={firstOrderID}"
                        : "";

                result.Paging.next = (lastOrderID != lastID)
                        ? result.Paging.next = $"http://localapp/api/clients/2/orders?idgt={lastOrderID}"
                        : "";
            }
            return result;
        }
    }
}