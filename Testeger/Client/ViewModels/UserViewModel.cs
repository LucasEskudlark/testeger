using System.ComponentModel.DataAnnotations;

namespace Testeger.Client.ViewModels;

public class UserViewModel
{
    [Required(ErrorMessage = "E-mail is required")]
    [EmailAddress(ErrorMessage = "Needs to be a valid e-mail")]
    public string Email { get; set; }

    //[Required(ErrorMessage = "Role is required")]
    //[EnumDataType(typeof(RoleType))]
    //public RoleType Role { get; set; }
}
