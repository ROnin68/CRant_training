using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Task_ASP.Web.Models.API
{
    public class OrderModel
    {
        public int ID { get; set; }
        public DateTime DateCreated { get; set; }
        public string Status { get; set; }
        public string Client { get; set; }
        public string Details { get; set; }
    }
}