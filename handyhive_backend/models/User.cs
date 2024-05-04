using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace handyhive_backend.models
{
    public class User
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public int Id { get; set; }
        [Required(ErrorMessage = "Email is required")]
        [EmailAddress(ErrorMessage = "Email must be a valid email address")]
        public string Email { get; set; } = string.Empty;

        [Required(ErrorMessage = "Password is required")]
        [RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[@$!%*?&])[A-Za-z\d@$!%*?&]{8,}$",
           ErrorMessage = "Password must contain at least one uppercase letter, one lowercase letter, one digit, one special character, and have a minimum length of 8")]
        public byte[] PasswordHash { get; set; }

        public byte[] PasswordSalt { get; set; }

        public string RefreshToken { get; set; } = string.Empty;

        public DateTime TokenCreated { get; set; }

        public DateTime TokenExpires { get; set; }
        public bool IsEmailConfirmed { get; set; }=false;
        [MaxLength(5)]
        public string ConfirmationCode { get; set; }
        public ERole Role { get; set; }
        [RegularExpression("^[A-Za-z]+$", ErrorMessage = "firstname must contain only alphabetical characters.")]
        public string Firstname { get; set; }
        [RegularExpression("^[A-Za-z]+$", ErrorMessage = "Lastname must contain only alphabetical characters.")]
        public string Lastname { get; set; }
        public string Phone { get; set; }
        public string? Profession { get; set; }
        public string? Address { get; set; }
        public string City { get; set; }
        public string Country { get; set; }
        public string State { get; set; }





    }


}
