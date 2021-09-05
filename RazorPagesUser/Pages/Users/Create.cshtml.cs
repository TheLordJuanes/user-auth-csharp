using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using RazorPagesUser.Data;
using RazorPagesUser.Models;

namespace RazorPagesUser.Pages.Users
{
    public class CreateModel : PageModel
    {
        private readonly RazorPagesUser.Data.RazorPagesUserContext _context;

        [BindProperty(SupportsGet = true)]
        public string ConfPassword { get; set; }
        [BindProperty(SupportsGet = true)]
        public string Password { get; set; }

        public CreateModel(RazorPagesUser.Data.RazorPagesUserContext context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
            return Page();
        }

        [BindProperty]
        public User User { get; set; }

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            if (ConfPassword.Equals(Password)) {
                _context.User.Add(User);
                await _context.SaveChangesAsync();
            }
            return RedirectToPage("Create");
        }
    }
}
