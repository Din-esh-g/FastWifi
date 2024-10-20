namespace Fastwifi.DTO
{
    public class ProgressNoteDto
    {     
            public string ConsumerName { get; set; }
            public string ProviderName { get; set; }
            public string CPSWCounty { get; set; }
            public string PathClient { get; set; }
            public string Participants { get; set; }
            public List<ServiceDetailDto> Services { get; set; }
            public List<InterventionSummaryDto> InterventionSummaries { get; set; }
            public string Response { get; set; }
            public string AdditionalInfo { get; set; }
            public string AuthorizedRep { get; set; }
            public string Signature { get; set; }
            public string Title { get; set; }
            public string ContactInfo { get; set; }
            public DateTime SignatureDate { get; set; }
            public string UserName { get; set; } // Username field
        }

        public class ServiceDetailDto
        {
            public DateTime Date { get; set; }
            public string Location { get; set; }
            public TimeSpan ServiceStartTime { get; set; }
            public TimeSpan ServiceStopTime { get; set; }
            public TimeSpan ITTStartTime { get; set; }
            public TimeSpan ITTStopTime { get; set; }
            public string MilesTraveled { get; set; }
            public string Locations { get; set; }
        }

        public class InterventionSummaryDto
        {
            public DateTime Date { get; set; }
            public string ALS { get; set; }
            public string IP { get; set; }
        }
    }

