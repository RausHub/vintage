using System.ComponentModel.DataAnnotations;

namespace VintageWheels.Models
{
    public class ContactFormViewModel
    {
        public ContactFormViewModel()
        {

        }

        public ContactFormViewModel(string subject): base()
        {
            this.Subject = subject;
        }


        [Required]
        public string Name { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        public string Message { get; set; }

        public string Subject { get; set; }
    }
}