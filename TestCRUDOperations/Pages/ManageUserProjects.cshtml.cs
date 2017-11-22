using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using TestCRUDOperations.DataAccess.Entities;
using TestCRUDOperations.DataAccess.Repository;

namespace TestCRUDOperations.Web.Pages
{
    public class ManageProjectUsersModel : PageModel
    {
        private readonly TestCRUDOperations.DataAccess.Context.ApplicationDbContext _context;
        [BindProperty(SupportsGet = true, Name = "userId")]
        public int Id { get; set; }
        
        public User User { get; set; }
        public ManageProjectUsersModel(TestCRUDOperations.DataAccess.Context.ApplicationDbContext context)
        {
            _context = context;
            IRepository<Project> repository = new Repository<Project>(_context);
            Projects = new List<Project>(repository.GetAll());

        }
        public IList<Project> Projects { get; set; }

        public async Task OnGetAsync()
        {
            IRepository<User> repository = new Repository<User>(_context);
            User = repository.Get(Id);
        }

        public string GetUserName()
        {
            string resultName = "Unknown";            
            if (User != null)
            {
                resultName = string.Format("{0} {1}", User.FirstName, User.LastName);
            }

            return resultName;
        }

        public void OnPostAddProjectToUser(int id)
        {               
            Update();
            IRepository<User> userRepository = new Repository<User>(_context);

            Project selectedProject = Projects.First(proj => proj.Id == id);
            UsersProjects usersProjects = new UsersProjects()
            {
               User = User,
               UserId = Id,
               Project = selectedProject,
               ProjectId = selectedProject.Id
            };
            User.Projects.Add(usersProjects);
            userRepository.Update(User);
        }

        public void OnPostRemoveProjectFromUser(int id)
        {
            Update();
            IRepository<User> repository = new Repository<User>(_context);
            foreach (UsersProjects usersProjects in User.Projects)
            {
                if (usersProjects.ProjectId == id)
                {
                    User.Projects.Remove(usersProjects);
                    break;
                }
            }
            repository.Update(User);
        }

        public void Update()
        {            
            IRepository<Project> projectRepository = new Repository<Project>(_context);
            Projects = new List<Project>(projectRepository.GetAll());
            IRepository<User> userRepository = new Repository<User>(_context);
            try
            {
                User = userRepository.Get(Id, user => user.Projects);
            }
            catch (InvalidOperationException e)
            {
                if (e.Message == "Sequence contains no elements")
                {
                    User = userRepository.Get(Id);
                }
                else
                {
                    throw;
                }
            }
            
        }

        public int GetCurrentUserId()
        {
            return Id;
        }
    }
}
