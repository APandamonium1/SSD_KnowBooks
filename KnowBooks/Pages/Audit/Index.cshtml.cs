using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using KnowBooks.Data;
using RazorPagesMovie.Models;

namespace KnowBooks.Pages.Audit
{
    public class IndexModel : PageModel
    {
        private readonly KnowBooks.Data.KnowBooksContext _context;

        public IndexModel(KnowBooks.Data.KnowBooksContext context)
        {
            _context = context;
        }

        public IList<AuditRecord> AuditRecord { get;set; } = default!;

        public async Task OnGetAsync()
        {
            if (_context.AuditRecords != null)
            {
                AuditRecord = await _context.AuditRecords.ToListAsync();
            }
        }
    }
}
