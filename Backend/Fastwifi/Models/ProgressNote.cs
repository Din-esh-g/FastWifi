using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Fastwifi.Models
{
    public class ProgressNote
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string ConsumerName { get; set; }
        public string ProviderName { get; set; }
        public string CPSWCounty { get; set; }
        public string PathClient { get; set; }
        public string Participants { get; set; }
        public List<ServiceDetail> Services { get; set; }
        public List<InterventionSummary> InterventionSummaries { get; set; }
        public string Response { get; set; }
        public string AdditionalInfo { get; set; }
        public string AuthorizedRep { get; set; }
        public string Signature { get; set; }
        public string Title { get; set; }
        public string ContactInfo { get; set; }
        public DateTime SignatureDate { get; set; }
        public string UserName { get; set; }  // For the user name in the request
    }
}
