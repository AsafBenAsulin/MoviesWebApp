using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using MoviesWebApp.Models;

namespace MoviesWebApp.Data
{
    public class MoviesDbContext : DbContext
    {
        public MoviesDbContext(DbContextOptions<MoviesDbContext> options)
            : base(options)
        {
        }

        public DbSet<Movie> Movies { get; set; }
        public DbSet<Category> Categories { get; set; }

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			modelBuilder.Entity<Movie>()
				.HasOne(m => m.Category)
				.WithMany()
				.HasForeignKey(m => m.CategoryId)
				.OnDelete(DeleteBehavior.Restrict);


			modelBuilder.Entity<Movie>()
				.HasOne(m => m.Category2)
				.WithMany()
				.HasForeignKey(m => m.CategoryId2)
				.OnDelete(DeleteBehavior.Restrict);


			modelBuilder.Entity<Category>().HasData(
				new { CategoryId = 1, Name = "Action" },
				new { CategoryId = 2, Name = "Science Fiction" },
				new { CategoryId = 3, Name = "Thriller" },
				new { CategoryId = 4, Name = "Comedy" },
				new { CategoryId = 5, Name = "Drama" }
			);

			modelBuilder.Entity<Movie>().HasData(
				new { Id = 1, Title = "Inception", Rating = 9, CategoryId = 3, CategoryId2 = 1 },
				new { Id = 2, Title = "The Dark Knight", Rating = 10, CategoryId = 3, CategoryId2 = 5 },
				new { Id = 3, Title = "Avatar", Rating = 8, CategoryId = 2, CategoryId2 = 1 },
				new { Id = 4, Title = "The Shawshank Redemption", Rating = 10, CategoryId = 3, CategoryId2 = 5 },
				new { Id = 5, Title = "Pulp Fiction", Rating = 5, CategoryId = 2, CategoryId2 = 1 },
				new { Id = 6, Title = "The Matrix", Rating = 7, CategoryId = 2, CategoryId2 = 3 }
			);

			base.OnModelCreating(modelBuilder);
		}

	}
}
