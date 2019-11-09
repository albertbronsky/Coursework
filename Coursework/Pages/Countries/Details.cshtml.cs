using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Coursework.Data;
using Coursework.Models;

namespace Coursework.Pages.Countries
{
    public class DetailsModel : PageModel
    {
        private readonly Coursework.Data.ApplicationDbContext _context;

        public DetailsModel(Coursework.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        public Country Country { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Country = await _context.Country.FirstOrDefaultAsync(m => m.CountryId == id);

            if (Country == null)
            {
                return NotFound();
            }
            return Page();
        }
    }
}
