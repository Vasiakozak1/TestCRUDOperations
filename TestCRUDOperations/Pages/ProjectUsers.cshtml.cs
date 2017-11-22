using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using TestCRUDOperations.DataAccess.Context;
using TestCRUDOperations.DataAccess.Entities;
using TestCRUDOperations.DataAccess.Repository;

namespace TestCRUDOperations.Web.Pages
{
    public class ProjectUsersModel : PageModel
    {
        private readonly TestCRUDOperations.DataAccess.Context.ApplicationDbContext _context;
        [BindProperty(Name = "projId", SupportsGet = true)]
        public int ProjectId { get; set; }
        public IList<User> Users { get; set; }
        public Project Project { get; set; }
        public ProjectUsersModel(TestCRUDOperations.DataAccess.Context.ApplicationDbContext context)
        {
            _context = context;
            Users = new List<User>();
        }


        public void OnGet()
        {
            IRepository<Project> projRepository = new Repository<Project>(_context);
            IRepository<User> userRepository = new Repository<User>(_context);
            Project = projRepository.Get(ProjectId, proj => proj.Users);

            foreach (UsersProjects usersProjects in Project.Users)
            {
                User tempUser = userRepository.Get(usersProjects.UserId);
                Users.Add(tempUser);
            }
        }
    }
}
