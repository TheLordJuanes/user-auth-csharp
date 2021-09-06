using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using RazorPagesUser.Models;

namespace RazorPagesUser.Pages.Users
{
    public class CreateModel : PageModel
    {
        private readonly RazorPagesUser.Data.RazorPagesUserContext _context;

        [BindProperty(SupportsGet = true)]
        public string ConfPassword { get; set; }

        public IList<User> UserList { get; set; }

        public CreateModel(RazorPagesUser.Data.RazorPagesUserContext context)
        {
            _context = context;
        }

        public async Task checkUsername()
        {
            var TotalUsers = from m in _context.User
                             select m;
            var user = TotalUsers;
            if (!string.IsNullOrEmpty(User.Username))
            {
                user = TotalUsers.Where(s => s.Username.Equals(User.Username));
            }
            UserList = await user.ToListAsync();
            if (UserList.Count == 1)
                ViewData["Message"] = "This username already exists!";
        }

        public IActionResult OnGet()
        {
            return Page();
        }

        [BindProperty]
        public new User User { get; set; }

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
            ViewData["Message"] = null;
            if (!ModelState.IsValid)
            {
                return Page();
            }

            if (ConfPassword.Equals(User.Password))
            {
                await checkUsername();
                if (ViewData["Message"] == null)
                {
                    _context.User.Add(User);
                    await _context.SaveChangesAsync();
                    return RedirectToPage("../Index");
                }
            } else
                ViewData["Message"] = "La constraseña no coincide";
            return RedirectToPage("../Index");
        }
    }
}
