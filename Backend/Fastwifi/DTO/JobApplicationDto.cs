namespace Fastwifi.DTO
{
    public class JobApplicationDto
    {
        public string Name { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public string Position { get; set; }
        public string Message { get; set; }
        public IFormFile Resume { get; set; }
    }
}
