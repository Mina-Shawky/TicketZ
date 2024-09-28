using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using Ticketz.Enum;

namespace Ticketz.Models;

public class Movie
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
    [Display(Name = "Movie Price")]
    public double Price { get; set; }

    [Required(ErrorMessage = "The Movie Picture is required.")]
    [Display(Name = "Movie Picture")]
    public byte[] Picture { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public Genre Genre { get; set; }
    public virtual List<Actor_Movie> Actor_Movie { get; set; }
    public int CinemaId {  get; set; }
    public virtual Cinema Cinema { get; set; }
    public int ProducerId { get; set; }
    public virtual Producer Producer { get; set; }
    public int NationalityId { get; set; }
    public virtual Nationality Nationality { get; set; }
}
