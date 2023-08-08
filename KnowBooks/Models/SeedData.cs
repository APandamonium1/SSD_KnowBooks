using Microsoft.EntityFrameworkCore;
using KnowBooks.Data;
using Microsoft.AspNetCore.Identity;


namespace KnowBooks.Models;

public class SeedData
{
	public static void Initialize(IServiceProvider serviceProvider)
    {
        using (var context = new KnowBooksContext(
            serviceProvider.GetRequiredService<
                DbContextOptions<KnowBooksContext>>()))
        {
			var userManager = serviceProvider.GetRequiredService<UserManager<ApplicationUser>>();
			var roleManager = serviceProvider.GetRequiredService<RoleManager<ApplicationRole>>();

			if (context == null || context.Book == null)
            {
                throw new ArgumentNullException("Null KnowBooksContext");
            }
			context.Database.Migrate();
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
                    Borrower = "",
                    ReturnDate = null
                },

                new Book
                {
                    Title = "The Power of Habit",
                    Author = "Charles Duhigg",
                    Genre = "Self-help",
                    AvailabilityStatus = "Loaned",
                    Borrower = "",
                    ReturnDate = null
                },

                new Book
                {
                    Title = "Outliers",
                    Author = "Malcolm Gladwell",
                    Genre = "Self-help",
                    AvailabilityStatus = "Available",
                    Borrower = "",
                    ReturnDate = null
                },

                new Book
                {
                    Title = "Afterlives",
                    Author = "Abdulrazak Gurnah",
                    Genre = "Fiction",
                    AvailabilityStatus = "Available",
                    Borrower = "",
                    ReturnDate = null
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
            if (!roleManager.RoleExistsAsync("Owner").GetAwaiter().GetResult())
            {
                var ownerRole = new ApplicationRole
                {
                    Id = "001",
                    Name = "Owner",
                    Description = "The role with access and permissions to everything. Can assign the role 'Admin' to users.",
                    CreatedDate = DateTime.Now,
                    NormalizedName = "OWNER",
                    ConcurrencyStamp = null
                };

                var adminRole = new ApplicationRole
                {
                    Id = "002",
                    Name = "Admin",
                    Description = "For assigning roles to other users.",
                    CreatedDate = DateTime.Now,
                    NormalizedName = "ADMIN",
                    ConcurrencyStamp = null
                };

                var staffRole = new ApplicationRole
                {
                    Id = "003",
                    Name = "Staff",
                    Description = "For managing books. CRUD book database.",
                    CreatedDate = DateTime.Now,
                    NormalizedName = "STAFF",
                    ConcurrencyStamp = null
                };

                var memberRole = new ApplicationRole
                {
                    Id = "004",
                    Name = "Member",
                    Description = "For normal users who just wants to loan and read books. They also need to return the books on time.",
                    CreatedDate = DateTime.Now,
                    NormalizedName = "MEMBER",
                    ConcurrencyStamp = null
                };

                var blacklistedRole = new ApplicationRole
                {
                    Id = "005",
                    Name = "Blacklisted",
                    Description = "Unable to access any functions on the website. Account is unable to log in to the website.",
                    CreatedDate = DateTime.Now,
                    NormalizedName = "BLACKLISTED",
                    ConcurrencyStamp = null
                };
                roleManager.CreateAsync(ownerRole).GetAwaiter().GetResult();
                roleManager.CreateAsync(adminRole).GetAwaiter().GetResult();
                roleManager.CreateAsync(staffRole).GetAwaiter().GetResult();
                roleManager.CreateAsync(memberRole).GetAwaiter().GetResult();
                roleManager.CreateAsync(blacklistedRole).GetAwaiter().GetResult();
            };

			if (context == null || context.Users == null)
			{
				throw new ArgumentNullException("Null KnowBooksContext");
			}

			if (!context.Users.Any())
			{
				// Seed user(s)
				var user = new ApplicationUser
				{
					Id = "Owner12345",
					FullName = "Owner",
					BirthDate = DateTime.Now,
					UserName = "owner@gmail.com",
					NormalizedUserName = "OWNER@GMAIL.COM",
					NormalizedEmail = "OWNER@GMAIL.COM",
					Email = "owner@gmail.com",
					EmailConfirmed = true,
					PhoneNumberConfirmed = false,
					TwoFactorEnabled = false,
					LockoutEnabled = true,
					AccessFailedCount = 0
				};

				// Create the user
				userManager.CreateAsync(user, "owner123").GetAwaiter().GetResult();

				// Assign roles to the user
				userManager.AddToRoleAsync(user, "Owner").GetAwaiter().GetResult();
		
			}



			context.SaveChanges();
        }

    }

}
