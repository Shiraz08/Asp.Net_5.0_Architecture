using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CAS_DataAccessLAyer_Project.Interface.IRepositoryWrapper
{
    public interface IRepositoryWrapper
    {
        //Define Interface Repository Name
        IStudentRepository student { get; }
        IUniversityStudent UniversityStudent { get; }
        ICourseRepository Course { get; }
        IDepartmentRepository Department { get; }

        //Define Custom Method
        Task SaveAsync();
    }
}
