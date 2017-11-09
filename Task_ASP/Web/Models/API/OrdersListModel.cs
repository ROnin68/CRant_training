using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Task_ASP.AppFacade.DTO;

namespace Task_ASP.Web.Models.API
{
    public class OrdersListModel
    {
        public List<DTO_Order> Data { get; set; }
        public PagingType Paging { get; private set; }

        public OrdersListModel()
        {
            this.Paging = new PagingType();
        }
    }

    public class PagingType
    {
        public string previous { get; set; }
        public string next { get; set; }
    }
}