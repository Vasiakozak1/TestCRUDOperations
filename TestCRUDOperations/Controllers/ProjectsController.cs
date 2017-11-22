using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TestCRUDOperations.DataAccess.Entities;
using TestCRUDOperations.Web.DTOs;
using TestCRUDOperations.DataAccess.Context;
using TestCRUDOperations.DataAccess.Repository;

namespace TestCRUDOperations.Web.Controllers
{
    [Produces("application/json")]
    [Route("api/Projects")]
    public class ProjectsController : Controller
    {        

        [HttpGet]
        [Route("Get")]
        public IEnumerable<ProjectDTO> GetAll()
        {
            List<ProjectDTO> projectsDtos = new List<ProjectDTO>();
            using (ApplicationDbContext context = new ApplicationDbContext())
            {
                IRepository<Project> repository = new Repository<Project>(context);
                var projects = repository.GetAll();
                foreach (Project project in projects)
                {
                    ProjectDTO dto = new ProjectDTO()
                    {
                        Name = project.Name,
                        Description = project.Description,

                        StartYear = project.StartDate.Year,
                        StartMonth = project.StartDate.Month,
                        StartDay = project.StartDate.Day,

                        EndYear = project.EndDate.Year,
                        EndMonth = project.EndDate.Month,
                        EndDay = project.EndDate.Day
                    };
                    projectsDtos.Add(dto);
                }
            }
            return projectsDtos;
        }
        
        [HttpGet]
        [Route("[action]/{id}")]
        public ProjectDTO Get(int id)
        {
            ProjectDTO projDTO = null;
            using (ApplicationDbContext context = new ApplicationDbContext())
            {
                IRepository<Project> repository = new Repository<Project>(context);
                Project project = repository.Get(id);
                projDTO = new ProjectDTO()
                {
                    Name = project.Name,
                    Description = project.Description,

                    StartYear = project.StartDate.Year,
                    StartMonth = project.StartDate.Month,
                    StartDay = project.StartDate.Day,

                    EndYear = project.EndDate.Year,
                    EndMonth = project.EndDate.Month,
                    EndDay = project.EndDate.Day
                };
            }
            return projDTO;
        }
        [HttpPost]
        [Route("[action]")]
        public void Create([FromBody]ProjectDTO projectDto)
        {
            using (ApplicationDbContext context = new ApplicationDbContext())
            {
                IRepository<Project> repository = new Repository<Project>(context);
                Project project = new Project()
                {
                    Name = projectDto.Name,
                    Description = projectDto.Description,
                    StartDate = new DateTime(projectDto.StartYear,projectDto.StartMonth,projectDto.StartDay),
                    EndDate = new DateTime(projectDto.EndYear,projectDto.EndMonth,projectDto.EndDay)
                };
                repository.Create(project);
            }
        }

        [HttpPost]
        [Route("[action]")]
        public void Update(ProjectDTO projectDto)
        {
            using (ApplicationDbContext context = new ApplicationDbContext())
            {
                IRepository<Project> repository = new Repository<Project>(context);
                Project project = new Project()
                {
                    Name = projectDto.Name,
                    Description = projectDto.Description,
                    StartDate = new DateTime(projectDto.StartYear, projectDto.StartMonth, projectDto.StartDay),
                    EndDate = new DateTime(projectDto.EndYear, projectDto.EndMonth, projectDto.EndDay)
                };
                repository.Update(project);
            }
        }

        [HttpDelete]
        [Route("[action]/{id}")]
        public void Delete(int id)
        {
            using (ApplicationDbContext context = new ApplicationDbContext())
            {
                IRepository<Project> repository = new Repository<Project>(context);
                repository.Delete(id);
            }
        }
    }
}