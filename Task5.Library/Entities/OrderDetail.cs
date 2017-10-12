using System.ComponentModel.DataAnnotations;

namespace Task5.Library.Entities
{
    public class OrderDetail
    {
        [Key]
        public int ID { get; set; }
        public int OrderID { get; set; }
        public int ProductID { get; set; }
        public int ProductQuantity { get; set; }

        //lazy loading added
        public virtual Order Order { get; set; }
        public virtual Product Product { get; set; }
    }
}