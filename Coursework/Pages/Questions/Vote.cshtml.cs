using Coursework.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace Coursework.Pages.Questions
{
    public class Vote : DI_BasePageModel
    {
        UserManager<IdentityUser> _userManager;

        public Vote(
            ApplicationDbContext context,
            IAuthorizationService authorizationService,
            UserManager<IdentityUser> userManager)
            : base(context, authorizationService, userManager)
        {
            _userManager = userManager;
        }

//        private readonly IBookFilterDropdownService _filterService;

//        public FilterModel(IBookFilterDropdownService filterService)
//        {
//            _filterService = filterService;
//        }

        public JsonResult OnGet(int id, int reaction)
        {
            var user = _userManager.GetUserId(User);

            Context.Database.ExecuteSqlRaw(
                $"INSERT INTO main.QuestionVote (OwnerId, Reaction, QuestionId) " +
                $"VALUES ('{user}', {reaction}, {id});");
            Context.Database.ExecuteSqlRaw(
                $"UPDATE main.Question SET Score = Score + {reaction} WHERE QuestionId = {id};");
            Context.SaveChangesAsync();
            
            return new JsonResult(reaction);
        }
    }
}