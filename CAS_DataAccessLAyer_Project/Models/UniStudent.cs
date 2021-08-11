using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CAS_DataAccessLAyer_Project.Models
{
   public class UniStudent
    {
        public int Id { get; set; } 
        public string UniStudentName { get; set; }
        public string UniStudentEmail { get; set; }
        public int UniStudentPhoneNo { get; set; }
        public int DepartmentId { get; set; }
        public int CourseId { get; set; }
    }

}
