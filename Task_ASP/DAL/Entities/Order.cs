using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Task_ASP.DAL.Entities
{
    public enum OrderStatus { osPending, osCancelled, osCompleted };

    public class Order
    {
        [Key]
        public int ID { get; set; }
        public int ClientID { get; set; }
        public DateTime DateCreated { get; set; }
        public OrderStatus Status { get; set; }
        //lazy loading added
        public virtual Client Client { get; set; }
        public virtual ICollection<OrderDetail> OrderDetails { get; set; }
    }
}