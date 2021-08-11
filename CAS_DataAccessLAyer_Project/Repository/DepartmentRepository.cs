using CAS_DataAccessLayer.Data;
using CAS_DataAccessLAyer_Project.Interface;
using CAS_DataAccessLAyer_Project.Models;
using CAS_DataAccessLAyer_Project.Repository.RepositoryBase;

namespace CAS_DataAccessLAyer_Project.Repository
{
    public class DepartmentRepository : RepositoryBase<Department>, IDepartmentRepository
    {
        public DepartmentRepository(context context)
       : base(context)
        {
        }

    }
}
