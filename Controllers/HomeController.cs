using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using MoviesWebApp.Models;
using MoviesWebApp.Services;
using System.Diagnostics;

namespace MoviesWebApp.Controllers
{
    public class HomeController : Controller
    {
        private IMovieService _movieService;
        public HomeController(IMovieService service)
        {
            _movieService = service;
        }

        public IActionResult Index()
        {
            return View(_movieService.GetMoviesByDescendingOrder());
        }

        // Get Home/Delete/5
        public IActionResult Delete(int id)
        {
            try
            {
                var movie = _movieService.GetMovieById(id);
                if (movie == null)
                {
                    throw new ArgumentException($"Movie with ID {id} not found.");
                }

                return View(movie);
            }
            catch (ArgumentException ex)
            {
                return RedirectToAction("Index", "Error", new { errorMessage = ex.Message });
            }
        }
		// Post Home/Delete/5
		[HttpPost, ActionName("Delete")]
        public IActionResult DeleteConfirmed(int id)
        {
            try
            {
                _movieService.DeleteMovie(id);
                return RedirectToAction("Index");
            }
            catch (ArgumentException ex)
            {
                return RedirectToAction("Index", "Error", new { errorMessage = ex.Message });
            }
        }
        // Get Home/Edit/5
        public IActionResult Edit(int id)
        {
            try
            {
                var movie = _movieService.GetMovieById(id);
                if (movie == null)
                {
                    throw new ArgumentException($"Movie with ID {id} not found.");
                }
                ViewBag.Categories = _movieService.GetAllCategories();
                return View(movie);
            }
            catch (ArgumentException ex)
            {
                return RedirectToAction("Index", "Error", new { errorMessage = ex.Message });
            }
        }
		// Post Home/Edit/5
		[HttpPost, ActionName("Edit")]
        public IActionResult EditConfirmed(Movie movie)
        {
            ModelState.Remove("Category");
			ModelState.Remove("Category2");
			if (!ModelState.IsValid)
            {
                ViewBag.Categories = _movieService.GetAllCategories();
                return View("Edit", movie);
            }

            try
            {
                _movieService.EditMovie(movie);
                return RedirectToAction("Index");
            }
            catch (ArgumentException ex)
            {
                return RedirectToAction("Index", "Error", new { errorMessage = ex.Message });
            }
        }


        // Get Home/Create
        public IActionResult Create(bool doesExist = false)
		{
			ViewBag.Error = doesExist;
			ViewBag.ErrorText = doesExist ? "There is a movie with this name already!" : string.Empty;
			ViewBag.Categories = _movieService.GetAllCategories();
			return View();
		}
        // Post Home/Create
        [HttpPost]
        public IActionResult Create(Movie movie)
        {
            ModelState.Remove("Category");
			ModelState.Remove("Category2");
			if (!ModelState.IsValid)
            {
                ViewBag.Categories = _movieService.GetAllCategories();
                ViewBag.Error = false;
                ViewBag.ErrorText = "There is a movie with this name already!";
                return View(movie);
            }

            if (_movieService.DoesMovieExist(movie.Title))
            {
                ViewBag.Error = true;
                ViewBag.ErrorText = "There is a movie with this name already!";
                return RedirectToAction("Create", new { doesExist = true });
            }


            _movieService.AddMovie(movie);
            return RedirectToAction("Index");
        }

        //Get Home/Search
        public IActionResult Search() 
        {
            return View();
        }
        //Post Home/ShowSearchResults
        public IActionResult ShowSearchResults(string SearchPhrase)
        {
            return View("Index",_movieService.SearchMoviesByString(SearchPhrase));
        }
    }
}