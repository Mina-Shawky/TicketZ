using System.ComponentModel.DataAnnotations;

namespace Ticketz.ViewModel;

public class AddNewActorViewModel
{
    [Required(ErrorMessage = "The Actor name is required.")]
    [Display(Name = "Actor Name")]
    [MaxLength(100)]
    public string Name { get; set; }

    [Required(ErrorMessage = "The Actor Picture is required.")]
    [Display(Name = "Actor Picture")]
    public IFormFile Picture { get; set; }

    [Required(ErrorMessage = "The Actor Bio is required.")]
    [Display(Name = "Actor Bio")]
    public string Bio { get; set; }
}
