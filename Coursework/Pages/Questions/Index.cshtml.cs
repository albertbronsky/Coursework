using System.Collections.Generic;
using System.Linq;
using Coursework.Data;
using Coursework.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace Coursework.Pages.Questions
{
    [AllowAnonymous]
    public class IndexModel : PageModel
    {
        private readonly ApplicationDbContext _context;

        public IndexModel(ApplicationDbContext context)
        {
            _context = context;
        }

        public IList<Question> Question { get;set; }

        public void OnGet()
        {
            Question = _context.Question.FromSqlRaw("SELECT * FROM main.Question;").ToList();
        }
    }
}
