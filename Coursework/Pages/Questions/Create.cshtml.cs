using System;
using System.Threading.Tasks;
using Coursework.Authorization;
using Coursework.Data;
using Coursework.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Coursework.Pages.Questions
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

        [BindProperty] public Question Question { get; set; }

        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            Question.OwnerId = UserManager.GetUserId(User);

            // requires using ContactManager.Authorization;
            var isAuthorized = await AuthorizationService.AuthorizeAsync(
                User, Question,
                QuestionOperations.Create);
            if (!isAuthorized.Succeeded)
            {
                return Forbid();
            }

            //Context.Question.Add(Question);

            var ownerId = Question.OwnerId;
            var title = Question.Title;
            var description = Question.Description;
            var dateCreated = DateTime.Now;
            const int defaultStatus = (int) QuestionStatus.Opened;

            Context.Database.ExecuteSqlRaw(
                $"INSERT INTO main.Question (OwnerId, Title, Description, DateCreated, DateModified, Status) " +
                $"VALUES ('{ownerId}', '{title}', '{description}', '{dateCreated}', NULL, {defaultStatus});");
            await Context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}