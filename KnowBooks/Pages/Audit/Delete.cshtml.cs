﻿using System;
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
    public class DeleteModel : PageModel
    {
        private readonly KnowBooks.Data.KnowBooksContext _context;

        public DeleteModel(KnowBooks.Data.KnowBooksContext context)
        {
            _context = context;
        }

        [BindProperty]
      public AuditRecord AuditRecord { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null || _context.AuditRecords == null)
            {
                return NotFound();
            }

            var auditrecord = await _context.AuditRecords.FirstOrDefaultAsync(m => m.Audit_ID == id);

            if (auditrecord == null)
            {
                return NotFound();
            }
            else 
            {
                AuditRecord = auditrecord;
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null || _context.AuditRecords == null)
            {
                return NotFound();
            }
            var auditrecord = await _context.AuditRecords.FindAsync(id);

            if (auditrecord != null)
            {
                AuditRecord = auditrecord;
                _context.AuditRecords.Remove(AuditRecord);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}
