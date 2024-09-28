using System.ComponentModel.DataAnnotations;

namespace Ticketz.ViewModel;

public class UpdateActorViewModel
{
    public int Id { get; set; }

    [Required(ErrorMessage = "The Actor name is required.")]
    [Display(Name = "Actor Name")]
    [MaxLength(100)]
    public string Name { get; set; }



    [Display(Name = "Actor Picture")]
    public IFormFile? Picture { get; set; }
    public byte[]? PictureCurrent { get; set; }




    [Required(ErrorMessage = "The Actor Bio is required.")]
    [Display(Name = "Actor Bio")]
    public string Bio { get; set; }
}
