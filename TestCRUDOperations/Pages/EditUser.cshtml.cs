using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using TestCRUDOperations.DataAccess.Context;
using TestCRUDOperations.DataAccess.Entities;
using TestCRUDOperations.DataAccess.Repository;

namespace TestCRUDOperations.Web.Pages
{
    public class EditUserModel : PageModel
    {
        private readonly TestCRUDOperations.DataAccess.Context.ApplicationDbContext _context;

        public EditUserModel(TestCRUDOperations.DataAccess.Context.ApplicationDbContext context)
        {
            _context = context;
        }
        [BindProperty(Name = "Id", SupportsGet = true)]
        public int UserId { get; set; }

        [BindProperty]
        public User User { get; set; }

        public IActionResult OnGet()
        {
            if (UserId == 0)
            {
                return NotFound();
            }
            IRepository<User> repository = new Repository<User>(_context);

            User = repository.Get(UserId, u => u.Projects);

            if (User == null)
            {
                return NotFound();
            }
            return Page();
        }

        public IActionResult OnPost()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }
            
            IRepository<User> repository = new Repository<User>(_context);          
            User updatedUser = repository.Get(UserId, u => u.Projects);//Navigation properties are required when update an entity
            updatedUser.Age = User.Age;
            updatedUser.FirstName = User.FirstName;
            updatedUser.LastName = User.LastName;
            repository.Update(updatedUser);           

            return RedirectToPage("./Users");
        }
    }
}

