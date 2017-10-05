using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Entities
{
    public enum OrderStatus { osPending, osCancelled, osCompleted };

    public class Order
    {
        [Key]
        public int ID { get; set; }
        [ForeignKey("ClientID")]

        public Client Client { get; set; }
        public int ClientID { get; set; }
        public DateTime DateCreated { get; set; }
        public OrderStatus Status { get; set; }
        public ICollection<OrderDetail> OrderDetails { get; set; }
    }
}