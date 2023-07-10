using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using KnowBooks.Models;

namespace KnowBooks.Data
{
    public class KnowBooksContext : DbContext
    {
        public KnowBooksContext (DbContextOptions<KnowBooksContext> options)
            : base(options)
        {
        }

        public DbSet<KnowBooks.Models.Book> Book { get; set; } = default!;
    }
}
