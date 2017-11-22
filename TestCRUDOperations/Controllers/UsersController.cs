using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TestCRUDOperations.DataAccess.Context;
using TestCRUDOperations.DataAccess.Entities;
using TestCRUDOperations.DataAccess.Repository;
using TestCRUDOperations.Web.DTOs;

namespace TestCRUDOperations.Web.Controllers
{
    [Produces("application/json")]
    [Route("api/Users")]
    public class UsersController : Controller
    {
        [HttpGet]
        [Route("Get")]
        public IEnumerable<UserDTO> GetAll()
        {
            List<UserDTO> userDtos = new List<UserDTO>();
            using (ApplicationDbContext context = new ApplicationDbContext())
            {
                IRepository<User> repository = new Repository<User>(context);
                var users = repository.GetAll();
                foreach (User user in users)
                {
                    UserDTO userDto = new UserDTO()
                    {
                        FirstName = user.FirstName,
                        LastName = user.LastName,
                        Age = user.Age
                    };
                    userDtos.Add(userDto);
                }
            }
            return userDtos;
        }

        [HttpGet]
        [Route("[action]/{id}")]
        public UserDTO Get(int id)
        {
            UserDTO userDto = null;
            using (ApplicationDbContext context = new ApplicationDbContext())
            {
                IRepository<User> repository = new Repository<User>(context);
                User user = repository.Get(id);
                userDto = new UserDTO()
                {
                    FirstName = user.FirstName,
                    LastName = user.LastName,
                    Age = user.Age
                };
            }
            return userDto;
        }

        [HttpPost]
        [Route("[action]")]
        public void Create([FromBody]UserDTO userDto)
        {
            using (ApplicationDbContext context = new ApplicationDbContext())
            {
                IRepository<User> repository = new Repository<User>(context);
                User user = new User()
                {
                    FirstName = userDto.FirstName,
                    LastName = userDto.LastName,
                    Age = userDto.Age
                };
                repository.Create(user);
            }
        }

        [HttpPost]
        [Route("[action]")]
        public void Update(UserDTO userDto)
        {
            using (ApplicationDbContext context = new ApplicationDbContext())
            {
                IRepository<User> repository = new Repository<User>(context);
                User user = new User()
                {
                    FirstName = userDto.FirstName,
                    LastName = userDto.LastName,
                    Age = userDto.Age
                };
                repository.Update(user);
            }
        }

        [HttpDelete]
        [Route("[action]/{id}")]
        public void Delete(int id)
        {
            using (ApplicationDbContext context = new ApplicationDbContext())
            {
                IRepository<User> repository = new Repository<User>(context);
                repository.Delete(id);
            }
        }
    }
}