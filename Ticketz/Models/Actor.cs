using System.ComponentModel.DataAnnotations;

namespace Ticketz.Models;

public class Actor
{
    [Key]
    public int Id { get; set; }

    [Required(ErrorMessage = "The Actor name is required.")]
    [Display(Name = "Actor Name")]
    [MaxLength(100)]
    public string Name { get; set; }

    [Required(ErrorMessage = "The Actor Picture is required.")]
    [Display(Name = "Actor Picture")]
    public byte[] Picture { get; set; }

    [Required(ErrorMessage = "The Actor Bio is required.")]
    [Display(Name = "Actor Bio")]
    public string Bio { get; set; }

    public virtual List<Actor_Movie> Actor_Movie {  get; set; }
}
