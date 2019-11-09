using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Coursework.Data;
using Coursework.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace Coursework.Pages.Questions
{
    [AllowAnonymous]
    public class DetailsModel : PageModel
    {
        private readonly ApplicationDbContext _context;

        public DetailsModel(ApplicationDbContext context)
        {
            _context = context;
        }

        public Question Question { get; set; }
        public List<Answer> Answer { get; set; }
        
        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Question = await _context.Question.FirstOrDefaultAsync(m => m.QuestionId == id);
            ViewData["AnswerCount"] = _context.Answer.FromSqlRaw($"SELECT * FROM main.Answer WHERE QuestionId={id} ").Count();
//            var answerCount = _context.Answer.FromSqlInterpolated($"SELECT * FROM main.Answer WHERE QuestionId=1;").ToList();
            Answer = _context.Answer.FromSqlRaw($"SELECT * FROM main.Answer WHERE QuestionId={id};").ToList();
//            ViewData["AnswerCount"] = answerCount;
            
            if (Question == null)
            {
                return NotFound();
            }
            return Page();
        }
    }
}
