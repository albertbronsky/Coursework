using Coursework.Data;
using Coursework.Pages.Questions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace Coursework.Areas.Identity.Pages.Account.Manage
{
    public class UpdateCountry : DI_BasePageModel
    {
        UserManager<IdentityUser> _userManager;

        public UpdateCountry(
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

        public JsonResult OnPost(int choice)
        {
            var user = _userManager.GetUserId(User);

            Context.Database.ExecuteSqlRaw($"UPDATE main.AspNetUsers SET Country={choice} WHERE Id='{user}';");
        
            return new JsonResult(choice);
        }

        public JsonResult OnGet()
        {
            var kek = Context.Country.FromSqlRaw("SELECT * FROM main.Country;").ToList();

//            Context.Database.ExecuteSqlRaw(
//                $"INSERT INTO main.AnswerVote (OwnerId, Reaction, AnswerId) " +
//                $"VALUES ('{user}', {reaction}, {id});");
//            Context.Database.ExecuteSqlRaw(
//                $"UPDATE main.Answer SET Score = Score + {reaction} WHERE AnswerId = {id};");
//            Context.SaveChangesAsync();
            
            return new JsonResult(kek);
        }
    }
}