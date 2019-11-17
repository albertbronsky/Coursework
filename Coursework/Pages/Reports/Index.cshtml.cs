using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using Coursework.Data;
using Coursework.Models;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using SQLitePCL;

namespace Coursework.Pages.Reports
{
    public class Index : PageModel
    {
        private readonly ApplicationDbContext _context;

        public Index(ApplicationDbContext context)
        {
            _context = context;
        }

        public IList<ActiveUser> ActiveUsers { get; set; }
        public IList<PopularQuestion> PopularQuestions { get; set; }
        public IList<TopAnswer> TopAnswers { get; set; }
        public IList<TopCountry> TopCountries { get; set; }

        public void OnGet()
        {
            ActiveUsers = _context.ActiveUsers.FromSqlRaw(
                @"SELECT UserName, COUNT (DISTINCT Answer.AnswerId) + COUNT (DISTINCT Question.QuestionId) AS NumberOfPosts 
                    FROM Question, AspNetUsers 
                    LEFT OUTER JOIN Answer ON Question.OwnerId = Answer.OwnerId 
                    WHERE Question.OwnerId = AspNetUsers.Id 
                    GROUP BY Question.OwnerId ORDER BY NumberOfPosts DESC;").ToList();
            
            PopularQuestions = _context.PopularQuestions.FromSqlRaw(
                @"SELECT Answer.QuestionId, Question.Title, Question.DateCreated, COUNT (DISTINCT Answer.AnswerId) AS NumberOfAnswers
                    FROM Answer
                    LEFT OUTER JOIN Question ON Answer.QuestionId = Question.QuestionId
                    WHERE Question.DateCreated LIKE '2019-11-16%' GROUP BY Answer.QuestionId
                    ORDER BY NumberOfAnswers DESC;").ToList();
            
            TopAnswers = _context.TopAnswers.FromSqlRaw(
                @"SELECT AnswerId, Answer.QuestionId, UserName, Message, Score 
                    FROM Answer, AspNetUsers
                    WHERE OwnerId = Id
                    ORDER BY Score DESC LIMIT 10;").ToList();
            
            TopCountries = _context.TopCountries.FromSqlRaw(
                @"SELECT Name, COUNT (DISTINCT Answer.AnswerId) + COUNT (DISTINCT Question.QuestionId) AS NumberOfPosts 
                    FROM Question, AspNetUsers, Country
                    LEFT OUTER JOIN Answer ON Question.OwnerId = Answer.OwnerId
                    WHERE Id = Question.OwnerId AND CountryId = Country
                    GROUP BY Name LIMIT 10;").ToList();
        }
    }
}