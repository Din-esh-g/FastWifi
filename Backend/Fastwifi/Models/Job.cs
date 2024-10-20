using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Fastwifi.Models
{
    public class Job
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string ?Location { get; set; }
        public string ?Salary { get; set; }
        public string ?Type { get; set; }
        public string ?Category { get; set; }
        public DateTime ?DatePosted { get; set; }
        public DateTime ?DateClosing { get; set; }


    }
}
