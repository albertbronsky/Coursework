using System;
using System.Globalization;
using System.Threading.Tasks;
using Coursework.Authorization;
using Coursework.Data;
using Coursework.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
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
            ViewData["CategoryId"] = new SelectList(Context.Category, "CategoryId", "Name");
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
            var today = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            Console.WriteLine(today);

            var ownerId = Question.OwnerId;
            var title = Question.Title;
            var description = Question.Description;
            var dateCreated = today;
            var categoryId = Question.CategoryId;
            const int defaultStatus = (int) QuestionStatus.Opened;

            Context.Database.ExecuteSqlRaw(
                $"INSERT INTO main.Question (OwnerId, Title, Description, DateCreated, DateModified, Status, CategoryId) " +
                $"VALUES ('{ownerId}', '{title}', '{description}', '{dateCreated}', NULL, {defaultStatus}, {categoryId});");
            await Context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}