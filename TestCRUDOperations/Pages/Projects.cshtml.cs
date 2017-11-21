using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging.Console.Internal;
using TestCRUDOperations.DataAccess.Context;
using TestCRUDOperations.DataAccess.Entities;
using TestCRUDOperations.DataAccess.Repository;

namespace TestCRUDOperations.Web.Pages
{
    public class ProjectsModel : PageModel
    {
        private readonly TestCRUDOperations.DataAccess.Context.ApplicationDbContext _context;

        public ProjectsModel(TestCRUDOperations.DataAccess.Context.ApplicationDbContext context)
        {
            _context = context;
        }

        public IList<Project> Projects { get;set; }

        public async Task OnGetAsync()
        {
            using (_context)
            {
                IRepository<Project> repository = new Repository<Project>(_context);
                Projects = new List<Project>(await repository.GetAllAsync());
            }
        }
    }
}
