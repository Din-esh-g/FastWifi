using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Fastwifi.Models
{
    public class ServiceDetail
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }
        public DateTime Date { get; set; }
        public string Location { get; set; }
        public TimeSpan ServiceStartTime { get; set; }
        public TimeSpan ServiceStopTime { get; set; }
        public TimeSpan ITTStartTime { get; set; }
        public TimeSpan ITTStopTime { get; set; }
        public string MilesTraveled { get; set; }
        public string Locations { get; set; }
    }
}
