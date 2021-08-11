using CAS_API_Project.Utilities;
using CAS_DataAccessLayer.Data;
using CAS_DataAccessLAyer_Project.Interface;
using CAS_DataAccessLAyer_Project.Models;
using CAS_DataAccessLAyer_Project.Repository.RepositoryBase;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CAS_DataAccessLAyer_Project.Repository
{
    public class StudentRepository : RepositoryBase<Student>, IStudentRepository
    {
        public StudentRepository(context context)
       : base(context)
        {
        }

    }
}
