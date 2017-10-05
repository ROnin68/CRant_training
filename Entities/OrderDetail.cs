using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Entities
{
    public class OrderDetail
    {
        [Key]
        public int ID { get; set; }
        [ForeignKey("OrderID")]
        public Order Order { get; set; }
        [ForeignKey("ProductID")]
        public Product Product { get; set; }

        public int OrderID { get; set; }
        public int ProductID { get; set; }
        public int ProductQuantity { get; set; }
    }

}