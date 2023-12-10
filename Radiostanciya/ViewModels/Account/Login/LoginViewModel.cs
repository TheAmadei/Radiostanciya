using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace Radiostanciya.ViewModels.Account.Login
{
    public class LoginViewModel
    {
        [Required]
        [Display(Name = "Username")]
        public string UserName { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }
    }
}
