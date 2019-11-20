using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Coursework.Data;
using Coursework.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace Coursework.Pages.Admin
{
    public class Index : PageModel
    {
        private readonly ApplicationDbContext _context;

        public Index(ApplicationDbContext context)
        {
            _context = context;
        }

        public IList<WeeklyRating> WeeklyRatings { get; set; }
        public IList<QuestionPerHour> QuestionPerHours { get; set; }

        public void OnGet()
        {
            WeeklyRatings = _context.WeeklyRatings.FromSqlRaw(
                @"SELECT Name, COUNT (Question.QuestionId) AS NumberOfPosts 
                    FROM Question, Category
                    WHERE Question.CategoryId = Category.CategoryId AND Question.DateCreated BETWEEN datetime('now', '-6 days') AND datetime('now', 'localtime')
                    GROUP BY Name LIMIT 5;").ToList();

            QuestionPerHours = _context.QuestionPerHours.FromSqlRaw(
                @"SELECT strftime ('%H',DateCreated) AS Hour, COUNT(Question.QuestionId) AS NumberOfQuestions
                    FROM Question GROUP BY strftime ('%H',DateCreated);").ToList();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            _context.Database.ExecuteSqlRaw(
                @"DELETE FROM Question WHERE Question.QuestionId = 
                    (SELECT Question.QuestionId FROM Question, Answer WHERE Question.DateCreated <= datetime('now', '-3 days') 
                    AND Question.QuestionId NOT IN (SELECT Question.QuestionId FROM Question, Answer WHERE Question.QuestionId = Answer.QuestionId 
                    GROUP BY Question.QuestionId) GROUP BY Question.QuestionId);"
            );
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}