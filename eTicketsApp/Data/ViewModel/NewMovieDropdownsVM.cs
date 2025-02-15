using eTicketsApp.Models;

namespace eTicketsApp.Data.ViewModel
{
    public class NewMovieDropdownsVM
    {
        public List<Actor> Actors { get; set; }
        public List<Producer> Producers { get; set; }
        public List<Cinema> Cinemas { get; set; }

        public NewMovieDropdownsVM()
        {
            Cinemas = new List<Cinema>();
            Producers = new List<Producer>();
            Actors = new List<Actor>();
        }
    }
}
