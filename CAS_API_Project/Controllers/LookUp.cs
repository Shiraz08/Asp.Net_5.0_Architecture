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
    [Route("api/DropDown")]
    [ApiController]
    public class LookUp : ControllerBase 
    {
        private readonly IRepositoryWrapper _repositoryWrapper;
        private readonly ILogger<LookUp> _logger;
        public LookUp(IRepositoryWrapper repositoryWrapper, ILogger<LookUp> logger)
        {
            _repositoryWrapper = repositoryWrapper;
            _logger = logger; 
        }


        [HttpGet("GetDepartmentList")]
        public IActionResult GetDepartmentList()
        {
            var UniversityList = _repositoryWrapper.Department.FindAll();
            _logger.LogInformation($"Completed : Item details for  {{{string.Join(", ", UniversityList)}}}");
            var std_list = from d in UniversityList
                           select new
                           {
                               Value = d.DepartmentId,
                               Text = d.DepartmentName
                           };
            return Ok(std_list.ToList());
        }
        [HttpGet("GetCourseList")]
        public IActionResult GetCourseList()
        {
            var CourseList = _repositoryWrapper.Course.FindAll();
            _logger.LogInformation($"Completed : Item details for  {{{string.Join(", ", CourseList)}}}");
            var std_list = from d in CourseList 
                           select new
                           {
                               Value = d.CourseId,
                               Text = d.CourseName
                           };
            return Ok(std_list.ToList());
        }
        [HttpPost("AddNewCourse")]
        public IActionResult AddNewCourse(Course st)
        {
            _repositoryWrapper.Course.Create(st);
            _repositoryWrapper.SaveAsync();
            return Ok(st);
        }
        [HttpPost("AddNewDepartment")]
        public IActionResult AddNewDepartment(Department st)
        {
            _repositoryWrapper.Department.Create(st);
            _repositoryWrapper.SaveAsync();
            return Ok(st);
        }
    }
}
