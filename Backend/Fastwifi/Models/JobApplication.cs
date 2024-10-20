using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Fastwifi.Models
{
    public class JobApplication
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string Name { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public string ?Position { get; set; }
        public string ?Message { get; set; }    
        public byte[] ?ResumeData { get; set; } // Store the resume as byte array
        public string ?ResumeFileName { get; set; } // Store the original file name
        public string ?ResumeContentType { get; set; } // Store the file type (e.g., application/pdf)
    }
}
