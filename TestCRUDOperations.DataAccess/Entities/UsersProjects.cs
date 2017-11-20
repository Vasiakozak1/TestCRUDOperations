using System;
using System.Collections.Generic;
using System.Text;

namespace TestCRUDOperations.DataAccess.Entities
{
    public class UsersProjects
    {
        public int UserId { get; set; }
        public int ProjectId { get; set; }

        public virtual User User { get; set; }
        public virtual Project Project { get; set; }
    }
}
