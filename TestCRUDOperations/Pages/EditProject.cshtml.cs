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
    public class EditProjectModel : PageModel
    {
        private readonly TestCRUDOperations.DataAccess.Context.ApplicationDbContext _context;

        public EditProjectModel(TestCRUDOperations.DataAccess.Context.ApplicationDbContext context)
        {
            _context = context;
        }
        [BindProperty(Name = "projId", SupportsGet = true)]
        public int ProjectId { get; set; }
        [BindProperty]
        public Project Project { get; set; }

        public IActionResult OnGet()
        {
            if (ProjectId == 0)
            {
                return NotFound();
            }
            IRepository<Project> repository = new Repository<Project>(_context);
            Project = repository.Get(ProjectId, proj => proj.Users);

            if (Project == null)
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

            IRepository<Project> repository = new Repository<Project>(_context);
            Project updatedProject = repository.Get(ProjectId, proj => proj.Users);
            updatedProject.Name = Project.Name;
            updatedProject.Description = Project.Description;
            updatedProject.StartDate = Project.StartDate;
            updatedProject.EndDate = Project.EndDate;
            repository.Update(updatedProject);

            return RedirectToPage("./Projects");
        }
    }
}
