using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Fastwifi.Models
{
    public class Comments
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }
        public string ?providedBy { get; set; }        
        public string ?Message { get; set; }
        public DateTime SentAt { get; set; }=DateTime.UtcNow;
    }
}
