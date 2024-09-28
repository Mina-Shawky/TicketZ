using System.ComponentModel.DataAnnotations;

namespace Ticketz.ViewModel;

public class RegisterViewModel
{
    [Display(Name = "First Name")]
    [Required]
    public string FirstName { get; set; }
    [Required]
    [Display(Name = "Last Name")]
    public string LastName { get; set; }
    [Required]
    [Display(Name = "Email")]
    [DataType(DataType.EmailAddress)]
    public string Email { get; set; }
    [Required]
    [Display(Name = "Password")]
    [DataType(DataType.Password)]
    public string Password { get; set; }
    [Required]
    [Display(Name = "ConfirmPassword")]
    [DataType(DataType.Password)]
    [Compare("Password")]
    public string ConfirmPassword { get; set; }

}
