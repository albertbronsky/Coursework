using System;
using System.Threading.Tasks;
using Coursework.Authorization;
using Coursework.Data;
using Coursework.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Coursework.Pages.Questions.Answers
{
    public class CreateModel : DI_BasePageModel
    {
        public CreateModel(ApplicationDbContext context,
            IAuthorizationService authorizationService,
            UserManager<IdentityUser> userManager)
            : base(context, authorizationService, userManager)
        {
            //_context = context;
        }

        public IActionResult OnGet()
        {
            return Page();
        }

        [BindProperty] public Answer Answer { get; set; }

        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync(int id)
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            Answer.OwnerId = UserManager.GetUserId(User);

            //Context.Question.Add(Question);
            var today = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");

            var ownerId = Answer.OwnerId;
            var message = Answer.Message;
            var dateCreated = today;

            Context.Database.ExecuteSqlRaw(
                "INSERT INTO main.Answer (OwnerId, Message, DateCreated, DateModified, QuestionId, Accepted, Score) " +
                $"VALUES ('{ownerId}', '{message}', '{dateCreated}', NULL, {id}, 0, 0);");
            await Context.SaveChangesAsync();

            return RedirectToPage("../Index");
        }
    }
}