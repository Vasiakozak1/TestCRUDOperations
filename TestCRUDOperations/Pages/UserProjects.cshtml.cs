using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using TestCRUDOperations.DataAccess.Context;
using TestCRUDOperations.DataAccess.Entities;
using TestCRUDOperations.DataAccess.Repository;

namespace TestCRUDOperations.Web.Pages
{
    public class UserProjectsModel : PageModel
    {
        private readonly TestCRUDOperations.DataAccess.Context.ApplicationDbContext _context;
        [BindProperty(SupportsGet = true, Name = "userId")]
        public int Id { get; set; }
        public User User { get; set; }
        public UserProjectsModel(ApplicationDbContext context)
        {
            _context = context;   
            Projects = new List<Project>();
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

        public IList<Project> Projects { get;set; }

        public async Task OnGetAsync()
        {
            IRepository<User> repository = new Repository<User>(_context);
            User = repository.Get(Id, u => u.Projects);
            IRepository<Project> projectsRepository = new Repository<Project>(_context);
            foreach (UsersProjects element in User.Projects)
            {
                Project projectToAdd = projectsRepository.Get(element.ProjectId);
                Projects.Add(projectToAdd);
            }
        }
    }
}
