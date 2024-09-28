using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using Ticketz.Enum;
using Ticketz.Models;

namespace Ticketz.ViewModel;

public class CreateMovieViewModel
{

    [Required(ErrorMessage = "The Movie Title is required.")]
    [Display(Name = "Movie Title")]
    [MaxLength(100)]
    public string Title { get; set; }

    [Required(ErrorMessage = "The Movie Description is required.")]
    [Display(Name = "Movie Description")]
    public string Description { get; set; }

    [Required(ErrorMessage = "The Movie Price is required.")]
    [Display(Name = "Movie Price $")]
    public double Price { get; set; }

    [Required(ErrorMessage = "The Movie Picture is required.")]
    [Display(Name = "Movie Picture")]
    public IFormFile Picture { get; set; }
    [Display(Name = "Start Date")]
    [Required]
    public DateTime StartDate { get; set; }
    [Display(Name = "End Date")]
    [Required]
    public DateTime EndDate { get; set; }
    [Required]
    public Genre Genre { get; set; }
    [Display(Name = "Actor(s)")]
    [Required]
    public List<int> Actor_Movie { get; set; }
    [Required]
    [Display(Name = "Cinema")]
    public int CinemaId { get; set; }
    [Display(Name = "Producer")]
    [Required]
    public int ProducerId { get; set; }
    [Display(Name = "Nationality")]
    [Required]
    public int NationalityId { get; set; }
}
