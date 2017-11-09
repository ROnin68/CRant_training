using System.Collections.Generic;
using Task_ASP.BL.DTO;
using Task_ASP.DAL.Entities;


namespace Task_ASP.BL.Mapping
{
    static class OrdersMapper
    {
        /// <summary>
        /// DAL.Entities.Order --> BL.DTO.DTO_Order
        /// </summary>
        /// <param name="order"></param>
        /// <returns></returns>
        public static DTO_Order ToManagerDTO_Order(this Order order)
        {
            return new DTO_Order()
            {
                ID = order.ID,
                DateCreated = order.DateCreated,
                Status = order.Status.ToString()
            };
        }


        public static List<DTO_Order> ToManagerOrdersList(this List<Order> ordersList)
        {
            var result = new List<DTO_Order>();
            foreach (var o in ordersList)
            {
                result.Add(o.ToManagerDTO_Order());
            }

            return result;
        }
    }
}
