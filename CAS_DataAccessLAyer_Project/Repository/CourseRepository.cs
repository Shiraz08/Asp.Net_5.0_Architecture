using CAS_DataAccessLayer.Data;
using CAS_DataAccessLAyer_Project.Interface;
using CAS_DataAccessLAyer_Project.Models;
using CAS_DataAccessLAyer_Project.Repository.RepositoryBase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CAS_DataAccessLAyer_Project.Repository
{
    public class CourseRepository : RepositoryBase<Course>, ICourseRepository
    {
        public CourseRepository(context context)
       : base(context)
        {
        }

    }
}
