using System.ComponentModel.DataAnnotations;

namespace MissingCode.Demo.Models
{
    public class ContactUsModel
    {
        [Required]
        public string Name { get; set; }
        [Required]
        [EmailAddress]
        public string Email { get; set; }
        public string Phone { get; set; }
    }
}