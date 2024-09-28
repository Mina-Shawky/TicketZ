using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace Ticketz.Models;

public class ApplicationUser : IdentityUser
{
    [Display(Name ="First Name")]
    public string FirstName {  get; set; }
    [Display(Name = "Last Name")]
    public string LastName { get; set; }
}
