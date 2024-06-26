﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using KnowBooks.Models;

namespace KnowBooks.Data
{
    public class KnowBooksContext :IdentityDbContext<ApplicationUser, ApplicationRole, string>
    {
        public KnowBooksContext (DbContextOptions<KnowBooksContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

			// Customize the ASP.NET Identity model and override the defaults if needed.
			// For example, you can rename the ASP.NET Identity table names and more.
			// Add your customizations after calling base.OnModelCreating(builder);
		}

        public DbSet<KnowBooks.Models.Book> Book { get; set; } = default!;
        public DbSet<RazorPagesMovie.Models.AuditRecord> AuditRecords { get; set; }
        public DbSet<KnowBooks.Models.Review> Review { get; set; } = default!;
    }
}
