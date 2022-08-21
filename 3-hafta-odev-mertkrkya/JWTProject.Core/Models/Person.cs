using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JWTProject.Core.Models
{
    public class Person : BaseModel
    {
        public int AccountId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Description { get; set; }
        public string Phone { get; set; }
        public DateTime DateOfBirth { get; set; }
        public Account Account { get; set; }
    }
}
