using System.ComponentModel.DataAnnotations;

namespace Ticketz.Models;

public class Producer
{
    [Key]
    public int Id { get; set; }
    [Required(ErrorMessage = "The Producer name is required.")]
    [Display(Name = "Producer Name")]
    [MaxLength(100)]
    public string Name { get; set; }
    [Required(ErrorMessage = "The Producer Picture is required.")]
    [Display(Name = "Producer Picture")]
    public byte[] Picture { get; set; }
    [Required(ErrorMessage = "The Producer Bio is required.")]
    [Display(Name = "Producer Bio")]
    public string Bio { get; set; }
}
