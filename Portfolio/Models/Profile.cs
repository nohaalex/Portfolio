using System.ComponentModel.DataAnnotations;

namespace Portfolio.Models
{
    public class Profile
    {
        [Required(ErrorMessage = "Please enter your name")]
        [DataType(DataType.Text)]
        public string Name { get; set; }


        [Required(ErrorMessage ="Please enter a valid Email")]
        [DataType(DataType.EmailAddress)]
        public string EmailAddress { get; set; }

        [Required(ErrorMessage ="Please enter the subject")]
        [DataType(DataType.Text)]
        public string Subject { get; set; }

        [Required(ErrorMessage ="Please enter you message")]
        [DataType(DataType.Text)]
        public string Body { get; set; }


    }
}
