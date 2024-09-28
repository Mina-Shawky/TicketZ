using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using Ticketz.Enum;
using Ticketz.Models;

namespace Ticketz.ViewModel;

public class UpdateMovieViewModel
{
    public int Id { get; set; }

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
    public byte[]? PictureCurrent { get; set; }
    [Display(Name = "Movie Picture")]
    public IFormFile? Picture { get; set; }
    [Display(Name = "Start Date")]
    [Required]
    public DateTime StartDate { get; set; }
    [Display(Name = "End Date")]
    [Required]
    public DateTime EndDate { get; set; }
    public Genre Genre { get; set; }
    [Display(Name = "Actor(s)")]
    [Required]
    public List<int> Actor_Movie { get; set; }
    [Display(Name = "Cinema")]
    [Required]
    public int CinemaId { get; set; }
    [Display(Name = "Producer")]
    [Required]
    public int ProducerId { get; set; }
    [Display(Name = "Nationality")]
    [Required]
    public int NationalityId { get; set; }
}
