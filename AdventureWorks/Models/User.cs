using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AdventureWorks.Models
{
    public class User
    {
        public string BusinessEntityID { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string JobTitle { get; set; }
        public string Department { get; set; }
        public string HireDate { get; set; }

        public string PhotoPath { get; set; }
    }
}
