using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using TestCRUDOperations.DataAccess.Context;
using TestCRUDOperations.DataAccess.Entities;

namespace TestCRUDOperations.Web.Pages
{
    public class UsersModel : PageModel
    {
        private readonly TestCRUDOperations.DataAccess.Context.ApplicationDbContext _context;

        public UsersModel(TestCRUDOperations.DataAccess.Context.ApplicationDbContext context)
        {
            _context = context;
        }

        public IList<User> Users { get;set; }

        public async Task OnGetAsync()
        {
            Users = await _context.Users.ToListAsync();
        }
    }
}
