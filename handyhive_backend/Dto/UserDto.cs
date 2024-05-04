using handyhive_backend.models;
using System.ComponentModel.DataAnnotations;

namespace handyhive_backend.Dto
{
    public class UserDto
    {
        [Required(ErrorMessage = "Email is required")]
        [EmailAddress(ErrorMessage = "Email must be a valid email address")]
        public string Email { get; set; } = string.Empty;

        [Required(ErrorMessage = "Password is required")]
        [RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[@$!%*?&])[A-Za-z\d@$!%*?&]{8,}$",
           ErrorMessage = "Password must contain at least one uppercase letter, one lowercase letter, one digit, one special character, and have a minimum length of 8")]
        public string Password { get; set; }       
        public ERole Role { get; set; }
        [RegularExpression("^[A-Za-z ]+$", ErrorMessage = "firstname must contain only alphabetical characters.")]
        public string Firstname { get; set; }
        [RegularExpression("^[A-Za-z ]+$", ErrorMessage = "Lastname must contain only alphabetical characters.")]
        public string Lastname { get; set; }
        public string Phone { get; set; }
        public string? Profession { get; set; }
        public string? Address { get; set; }
        public string City { get; set; }
        public string Country { get; set; }
        public string State { get; set; }
    }
}
