using Microsoft.EntityFrameworkCore;
using MoviesWebApp.Data;
using MoviesWebApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MoviesWebApp.Services
{
    public class MovieService : IMovieService
    {
        private readonly MoviesDbContext _dbContext;

        public MovieService(MoviesDbContext context)
        {
            _dbContext = context;
        }

        public void AddMovie(Movie movie)
        {
            if (movie == null)
            {
                throw new ArgumentNullException(nameof(movie));
            }

            _dbContext.Add(movie);
            _dbContext.SaveChanges();
        }

        public void DeleteMovie(int id)
        {
            var movie = GetMovieById(id);
            if (movie != null)
            {
                _dbContext.Remove(movie);
                _dbContext.SaveChanges();
            }
            else
            {
                throw new ArgumentException($"Movie with id {id} not found.");
            }
        }

        public void EditMovie(Movie movie)
        {
            if (movie == null)
            {
                throw new ArgumentNullException(nameof(movie));
            }

            _dbContext.Update(movie);
            _dbContext.SaveChanges();
        }

        public List<Category> GetAllCategories()
        {
            return _dbContext.Categories.ToList();
        }

        public Movie GetMovieById(int id)
        {
            return _dbContext.Movies
                .Include(m => m.Category)
                .Include(m => m.Category2)
                .SingleOrDefault(m => m.Id == id);
        }

        public List<Movie> GetMoviesByDescendingOrder()
        {
            return _dbContext.Movies
                .Include(m => m.Category)
                .Include(m => m.Category2)
                .OrderByDescending(m => m.Rating)
                .ToList();
        }

        public bool DoesMovieExist(string title)
        {
            return _dbContext.Movies.Any(m => m.Title == title);
        }

        public List<Movie> SearchMoviesByString(string searchPhrase)
        {
            return _dbContext.Movies
                .Include(m => m.Category)
                .Include(m => m.Category2)
                .Where(m => m.Title.ToLower().Contains(searchPhrase.ToLower()))
                .OrderByDescending(m => m.Rating)
                .ToList();
        }

    }
}
