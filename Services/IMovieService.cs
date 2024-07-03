using MoviesWebApp.Models;

namespace MoviesWebApp.Services
{
    public interface IMovieService
    {
        public List<Movie> GetMoviesByDescendingOrder();
        public void AddMovie(Movie movie);
        public void EditMovie(Movie movie);
        public void DeleteMovie(int id);
        public Movie GetMovieById(int id);
        public List<Category> GetAllCategories();
        public bool DoesMovieExist(string title);
        public List<Movie> SearchMoviesByString(string SearchPhrase);
    }
}
