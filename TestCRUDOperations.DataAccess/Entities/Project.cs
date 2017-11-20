using System;
using System.Collections.Generic;
using TestCRUDOperations.DataAccess.Repository;

namespace TestCRUDOperations.DataAccess.Entities
{
    public class Project: IEntity
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public ICollection<UsersProjects> Users { get; set; } = new List<UsersProjects>();
    }
}
