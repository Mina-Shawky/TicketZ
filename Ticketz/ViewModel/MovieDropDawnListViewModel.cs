using Ticketz.Models;

namespace Ticketz.ViewModel;

public class MovieDropDawnListViewModel
{
    public MovieDropDawnListViewModel()
    {
        ProducerList = new List<Producer>();
        CinemaList = new List<Cinema>();
        ActorList = new List<Actor>();
        NationalityList = new List<Nationality>();
    }
    public List<Cinema> CinemaList { get; set; }
    public List<Actor> ActorList { get;set; }
    public List<Nationality> NationalityList { get;set; }
    public List<Producer> ProducerList {  get; set; }

}
