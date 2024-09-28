using System.ComponentModel.DataAnnotations;

namespace Ticketz.ViewModel;

public class UpdateCinemaViewModel
{
    public int Id { get; set; } 

    [Required(ErrorMessage = "The Cinema name is required.")]
    [Display(Name = "Cinema Name")]
    [MaxLength(100)]
    public string Name { get; set; }
    [Display(Name = "Cinema Logo")]
    public IFormFile? Logo { get; set; }
    public byte[]? LogoCurrent { get; set; }

    [Required(ErrorMessage = "The Cinema Description is required.")]
    [Display(Name = "Cinema Description")]
    public string Description { get; set; }
}
