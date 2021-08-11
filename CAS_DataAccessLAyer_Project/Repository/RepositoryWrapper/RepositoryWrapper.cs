using CAS_DataAccessLayer.Data;
using CAS_DataAccessLAyer_Project.Interface;
using CAS_DataAccessLAyer_Project.Interface.IRepositoryWrapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CAS_DataAccessLAyer_Project.Repository.RepositoryWrapper
{
    public class RepositoryWrapper : IRepositoryWrapper
    {
        private readonly context _context;
        public RepositoryWrapper(context context)
        {
            _context = context;
        }

        public IStudentRepository _student { get; set; }
        public IStudentRepository student
        {
            get
            {
                if (_student == null)
                {
                    _student = new StudentRepository(_context);
                }
                return _student;
            }
        }

        public IUniversityStudent _UniversityStudent { get; set; }
        public IUniversityStudent UniversityStudent
        {
            get
            {
                if (_UniversityStudent == null)
                {
                    _UniversityStudent = new UniversityStudentRepository(_context);
                }
                return _UniversityStudent;
            }
        }

        public ICourseRepository _Course { get; set; }
        public ICourseRepository Course 
        {
            get
            {
                if (_Course == null)
                {
                    _Course = new CourseRepository(_context);
                }
                return _Course;
            }
        }

        public IDepartmentRepository _Department { get; set; }
        public IDepartmentRepository Department
        {
            get
            {
                if (_Department == null)
                {
                    _Department = new DepartmentRepository(_context);
                }
                return _Department;
            }
        }

        public async Task SaveAsync()
        {
            await _context.SaveChangesAsync();
        }


    }
}
