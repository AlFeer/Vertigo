using System.ComponentModel.DataAnnotations;

namespace Hotel.ViewModel
{
    public class LoginVM
    {
        [Required, MaxLength(100), DataType(DataType.EmailAddress)]
        public string Email { get; set; }
        [Required, MaxLength(100), DataType(DataType.Password)]
        public string Password { get; set; }
    }
}
