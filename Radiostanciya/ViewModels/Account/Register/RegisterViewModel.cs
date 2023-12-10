

using System.ComponentModel.DataAnnotations;

namespace Radiostanciya.ViewModels.Account.Register
{
    public class RegisterViewModel
    {
        [Required(ErrorMessage = "Укажите имя")]
        [MaxLength(15, ErrorMessage = "Имя пользователя должно быть не более 15 символов")]
        [MinLength(5, ErrorMessage = "Имя пользователя должно быть не менее 5 символов")]
        [Display(Name = "Username")]
        public string UserName { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirm Password")]
        [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; }
    }
}
