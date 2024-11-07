namespace Fastwifi.DTO
{
    public class JobApplicationReturnDTO
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public string ?Position { get; set; }
        public string ?Message { get; set; }
        public string ?ResumeFileName { get; set; }
    }
}
