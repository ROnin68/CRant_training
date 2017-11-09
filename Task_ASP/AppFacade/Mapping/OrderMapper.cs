using System.Collections.Generic;

namespace Task_ASP.AppFacade.Mapping
{
    static class OrderMapper
    {
        /// <summary>
        /// BL.DTO.DTO_Order --> AppFacade.DTO.DTO_Order
        /// </summary>
        /// <param name="blOrder"></param>
        /// <returns></returns>
        public static DTO.DTO_Order ToFacadeOrder(this BL.DTO.DTO_Order blOrder)
        {
            return new DTO.DTO_Order()
            {
                ID = blOrder.ID,
                DateCreated = blOrder.DateCreated,
                Status = blOrder.Status
            };
        }
        /// <summary>
        ///  AppFacade.DTO.DTO_Order --> BL.DTO.DTO_Order 
        /// </summary>
        /// <param name="mngOrder"></param>
        /// <returns></returns>
        public static BL.DTO.DTO_Order ToManagerOrder(this DTO.DTO_Order mngOrder)
        {
            return new BL.DTO.DTO_Order()
            {
                ID = mngOrder.ID,
                DateCreated = mngOrder.DateCreated,
                Status = mngOrder.Status
            };
        }

        public static List<DTO.DTO_Order> ToFacadeOrdersList(this List<BL.DTO.DTO_Order> orderList)
        {
            var result = new List<DTO.DTO_Order>();
            foreach (var o in orderList)
            {
                result.Add(o.ToFacadeOrder());
            }

            return result;
        }
    }
}
