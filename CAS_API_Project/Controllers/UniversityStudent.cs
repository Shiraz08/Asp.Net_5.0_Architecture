using CAS_DataAccessLAyer_Project;
using CAS_DataAccessLAyer_Project.Interface.IRepositoryWrapper;
using CAS_DataAccessLAyer_Project.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CAS_API_Project.Controllers
{
    [Route("api/UniversityStudent")]
    [ApiController]
    public class UniversityStudent : ControllerBase
    {
        private readonly IRepositoryWrapper _repositoryWrapper;
        private readonly ILogger<UniversityStudent> _logger;
        public UniversityStudent(IRepositoryWrapper repositoryWrapper, ILogger<UniversityStudent> logger) 
        {
            _repositoryWrapper = repositoryWrapper;
            _logger = logger;
        }

        [HttpGet("GetUniversityStudentList")]
        public IActionResult GetStudentList()
        {

            var stdlist = from std in _repositoryWrapper.UniversityStudent.FindAll()
                          join dep in _repositoryWrapper.Department.FindAll() on std.DepartmentId equals dep.DepartmentId
                          join cou in _repositoryWrapper.Course.FindAll() on std.CourseId equals cou.CourseId
                          select new ViewModel
                          {
                              Id = std.Id,
                              UniStudentName = std.UniStudentName,
                              UniStudentEmail = std.UniStudentEmail,
                              UniStudentPhoneNo = std.UniStudentPhoneNo,
                              DepartmentName = dep.DepartmentName,
                              CourseName = cou.CourseName
                          };
            _logger.LogInformation($"Completed : Item details for  {{{string.Join(", ", stdlist)}}}");
            return Ok(stdlist.ToList());
        }
        [HttpPost("AddUniversityStudent")]
        public IActionResult AddUniversityStudent(UniStudent st)
        {
            _repositoryWrapper.UniversityStudent.Create(st);
            _repositoryWrapper.SaveAsync();
            return Ok(st);
        }
        [HttpPost("UpdateUniversityStudent")]
        public IActionResult UpdateUniversityStudent(UniStudent st)
        {
            _repositoryWrapper.UniversityStudent.Update(st);
            _repositoryWrapper.SaveAsync();
            return Ok(st);
        }
        [HttpPost("DeleteUniversityStudent")]
        public IActionResult DeleteUniversityStudent(UniStudent st)
        {
            _repositoryWrapper.UniversityStudent.Delete(st);
            _repositoryWrapper.SaveAsync();
            return Ok(st);
        }
    }
} 
