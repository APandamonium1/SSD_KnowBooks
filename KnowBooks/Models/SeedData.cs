﻿using Microsoft.EntityFrameworkCore;
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
            context.SaveChanges();
        }
    }
}
