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
    public class ManageUsersOfProjectModel : PageModel
    {
        private readonly TestCRUDOperations.DataAccess.Context.ApplicationDbContext _context;

        public ManageUsersOfProjectModel(TestCRUDOperations.DataAccess.Context.ApplicationDbContext context)
        {
            _context = context;
            Users = new List<User>();
        }
        [BindProperty(Name = "projId", SupportsGet = true)]
        public int ProjectId { get; set; }
        public Project Project { get; set; }
        public IList<User> Users { get;set; }

        public async Task<IActionResult> OnGetAsync()
        {
            if (Update())
                return Page();
            return NotFound();
        }

        public string GetProjectName()
        {
            if (Project == null)
            {
                return "Unknown";
            }
            else
            {
                return Project.Name;
            }
        }

        public void OnPostAddUserToProject(int userIndex)
        {
            Update();
            IRepository<Project> projRepository = new Repository<Project>(_context);
            User selectedUser = Users.First(u => u.Id == userIndex);
            UsersProjects userProject = new UsersProjects()
            {
                User = selectedUser,
                UserId = selectedUser.Id,
                Project = Project,
                ProjectId = ProjectId
            };
            Project.Users.Add(userProject);
            projRepository.Update(Project);
        }

        public void OnPostRemoveUserFromProject(int userIndex)
        {
            Update();
            IRepository<Project> projRepository = new Repository<Project>(_context);
            User selecteduser = Users.First(u => u.Id == userIndex);
            foreach (UsersProjects userProject in Project.Users)
            {
                if (userProject.UserId == selecteduser.Id)
                {
                    Project.Users.Remove(userProject);
                    break;
                }
            }
        }

        private bool Update()
        {
            IRepository<User> userRepository = new Repository<User>(_context);
            Users = new List<User>(userRepository.GetAll());
            IRepository<Project> projRepository = new Repository<Project>(_context);
            try
            {
                Project = projRepository.Get(ProjectId, proj => proj.Users);
            }
            catch (InvalidOperationException e)
            {
                if (e.Message == "Sequence contains no elements")
                {
                    Project = projRepository.Get(ProjectId);
                }
                else
                {
                    throw;
                }
            }
            return true;
        }
    }
}
