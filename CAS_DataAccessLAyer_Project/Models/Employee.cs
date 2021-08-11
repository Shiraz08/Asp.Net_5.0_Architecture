using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CAS_DataAccessLAyer_Project.Models
{
   public class Employee
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public int PhoneNo { get; set; }
    }
}
