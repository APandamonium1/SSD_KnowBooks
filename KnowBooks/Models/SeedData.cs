using Microsoft.EntityFrameworkCore;
using KnowBooks.Data;

namespace KnowBooks.Models;

public static class SeedData
{
    public static void Initialize(IServiceProvider serviceProvider)
    {
        using (var context = new KnowBooksContext(
            serviceProvider.GetRequiredService<
                DbContextOptions<KnowBooksContext>>()))
        {
            if (context == null || context.Book == null)
            {
                throw new ArgumentNullException("Null KnowBooksContext");
            }

            // Look for any books.
            if (context.Book.Any())
            {
                return;   // DB has been seeded
            }

            context.Book.AddRange(
                new Book
                {
                    Title = "Meditations",
                    Author = "Marcus Aurelius",
                    Genre = "Philosophy",
                    AvailabilityStatus = "Available",
                },

                new Book
                {
                    Title = "The Power of Habit",
                    Author = "Charles Duhigg",
                    Genre = "Self-help",
                    AvailabilityStatus = "Loaned",
                },

                new Book
                {
                    Title = "Outliers",
                    Author = "Malcolm Gladwell",
                    Genre = "Self-help",
                    AvailabilityStatus = "Available"
                },

                new Book
                {
                    Title = "Afterlifes",
                    Author = "Abdulrazak Gurnah",
                    Genre = "Fiction",
                    AvailabilityStatus = "Available"
                }
            );



            if (context == null || context.Roles == null)
            {
                throw new ArgumentNullException("Null KnowBooksContext");
            }
            if (context.Roles.Any())
            {
                return;   // DB has been seeded
            }
            context.Roles.AddRange(
                new ApplicationRole
                {
                    Id = "001",
                    Name = "Admin",
                    Description = "Admin role",
                    CreatedDate = DateTime.Now,
                    NormalizedName = "ADMIN",
                    ConcurrencyStamp = null
                },

                 new ApplicationRole
                 {
                     Id = "002",
                     Name = "Staff",
                     Description = "Staff role",
                     CreatedDate = DateTime.Now,
                     NormalizedName = "STAFF",
                     ConcurrencyStamp = null
                 },

                new ApplicationRole
                {
	                Id = "003",
	                Name = "User",
	                Description = "User role",
	                CreatedDate = DateTime.Now,
	                NormalizedName = "USER",
	                ConcurrencyStamp = null
                }


			);

			if (context == null || context.Users == null)
			{
				throw new ArgumentNullException("Null KnowBooksContext");
			}

			if (context.Users.Any())
			{
				return;   // DB has been seeded
			}

			

		   context.SaveChanges();
        }
    }
}
