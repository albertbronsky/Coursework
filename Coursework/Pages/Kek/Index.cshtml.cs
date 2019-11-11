using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Coursework.Data;
using Coursework.Models;

namespace Coursework.Pages.Kek
{
    public class IndexModel : PageModel
    {
        private readonly Coursework.Data.ApplicationDbContext _context;

        public IndexModel(Coursework.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        public IList<Question> Question { get;set; }

        public async Task OnGetAsync()
        {
            Question = await _context.Question
                .Include(q => q.Category).ToListAsync();
        }
    }
}
