﻿using System.ComponentModel.DataAnnotations;

namespace Ticketz.ViewModel;

public class AddCinemaViewModel
{
    [Required(ErrorMessage = "The Cinema name is required.")]
    [Display(Name = "Cinema Name")]
    [MaxLength(100)]
    public string Name { get; set; }
    [Required(ErrorMessage = "The Cinema Logo is required.")]
    [Display(Name = "Cinema Logo")]
    public IFormFile Logo { get; set; }
    [Required(ErrorMessage = "The Cinema Description is required.")]
    [Display(Name = "Cinema Description")]
    public string Description { get; set; }
}
