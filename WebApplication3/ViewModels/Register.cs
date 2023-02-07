using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;
using ApplicationSecurity.RegexChecker;

namespace WebApplication3.ViewModels
{
    public class Register
    {
        [Required]
        [DataType(DataType.Text)]
        [RegularExpression(RegexChecker.textchecker, ErrorMessage = "Do not input Special Characters")]
        public string FullName { get; set; }

        [Required]
        [MinLength(16)]
        [MaxLength(16)]
        [DataType(DataType.CreditCard)]
        [RegularExpression(RegexChecker.numberchecker, ErrorMessage = "Only numbers allowed")]
        public string CreditCardNo { get; set; }

        [Required]
        [DataType(DataType.Text)]
        public string Gender { get; set; }

        [Required]
        [MaxLength(8)]
        [MinLength(8)]
        [DataType(DataType.PhoneNumber)]
        [RegularExpression(RegexChecker.numberchecker, ErrorMessage = "Only Numbers allowed")]
        public string MobileNumber { get; set; }

        [Required]
        [DataType(DataType.Text)]
        [RegularExpression(RegexChecker.textchecker, ErrorMessage = "Do not input Special Characters")]
        public string Address { get; set; }

        [Required]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Compare(nameof(Password), ErrorMessage = "Password and confirmation password does not match")]
        public string ConfirmPassword { get; set; }

        [Required]
        [DataType(DataType.Text)]
        public string AboutMe { get; set; }

        [Required]
        public IFormFile? Photo { get; set; }


    }
}
