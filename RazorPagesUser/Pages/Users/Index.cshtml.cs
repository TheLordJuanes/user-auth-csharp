using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using RazorPagesUser.Models;


namespace RazorPagesUser.Pages.Users
{
    public class IndexModel : PageModel
    {
        private readonly RazorPagesUser.Data.RazorPagesUserContext _context;

        public IndexModel(RazorPagesUser.Data.RazorPagesUserContext context)
        {
            _context = context;
        }

        [BindProperty(SupportsGet = true)]
        public string SearchString { get; set; }

        [BindProperty(SupportsGet = true)]
        public string Passwordp { get; set; }

        public new IList<User> User { get;set; }

        public async Task OnGetAsync()
        {
            var TotalUsers = from m in _context.User
                             select m;
            var user = TotalUsers;
            if (!string.IsNullOrEmpty(SearchString))
            {
                user = TotalUsers.Where(s => s.Username.Equals(SearchString));
            }
            User = await user.ToListAsync();
            if (User.Count == 1)
            {
                if (User.ElementAt(0).Password.Equals(Passwordp) && !string.IsNullOrEmpty(Passwordp))
                {
                    ViewData["Message"] = User.ElementAt(0).Username;
                    User = await TotalUsers.ToListAsync();
                }
                else
                {
                    Response.Redirect("../Index");
                }
            }
            else
            {
                Response.Redirect("../Index");
            }
        }
    }
}
