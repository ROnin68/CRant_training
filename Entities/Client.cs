using System.ComponentModel.DataAnnotations;

namespace Entities
{
    public class Client
    {
        [Key]
        public int ID { get; set; }
        public string Name { get; set; }
    }

}