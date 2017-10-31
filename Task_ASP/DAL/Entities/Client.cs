using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Task_ASP.DAL.Entities
{
    public class Client
    {
        [Key]
        public int ID { get; set; }
        public string Name { get; set; }
        public bool IsDeleted { get; set; }
        //lazy loading added
        public virtual ICollection<Order> Orders { get; set; }
    }
}