using System.ComponentModel.DataAnnotations;

namespace Ticketz.ViewModel;

public class AddNewProducerViewModel
{
    [Required(ErrorMessage = "The Producer name is required.")]
    [Display(Name = "Producer Name")]
    [MaxLength(100)]
    public string Name { get; set; }
    [Required(ErrorMessage = "The Producer Picture is required.")]
    [Display(Name = "Producer Picture")]
    public IFormFile Picture { get; set; }
    [Required(ErrorMessage = "The Producer Bio is required.")]
    [Display(Name = "Producer Bio")]
    public string Bio { get; set; }
}
