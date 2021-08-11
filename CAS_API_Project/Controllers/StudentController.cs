using CAS_DataAccessLAyer_Project.Interface.IRepositoryWrapper;
using CAS_DataAccessLAyer_Project.Models;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;

namespace CAS_API_Project.Controllers
{
    [Route("api/Student")]
    [ApiController]
    public class StudentController : ControllerBase
    {
        private readonly IRepositoryWrapper _repositoryWrapper;
        private readonly ILogger<StudentController> _logger;
        public StudentController(IRepositoryWrapper repositoryWrapper, ILogger<StudentController> logger)
        {
            _repositoryWrapper = repositoryWrapper;
            _logger = logger;
        }

        [HttpPost("AddStudent")]
        public IActionResult AddStudent(Student st)
        {
            _repositoryWrapper.student.Create(st);
            _repositoryWrapper.SaveAsync();
            return Ok(st);
        }
        [HttpPost("UpdateStudent")]
        public IActionResult UpdateStudent(Student st)
        {
            _repositoryWrapper.student.Update(st);
            _repositoryWrapper.SaveAsync();
            return Ok(st);
        }
        [HttpPost("DeleteStudent")]
        public IActionResult DeleteStudent(Student st)
        {
            _repositoryWrapper.student.Delete(st);
            _repositoryWrapper.SaveAsync();
            return Ok(st);
        }

        [HttpGet("GetStudentList")]
        public IActionResult GetStudentList()
        {
           var studentsList = _repositoryWrapper.student.FindAll();
            _logger.LogInformation($"Completed : Item details for  {{{string.Join(", ", studentsList)}}}");
            return Ok(studentsList.ToList());
        }
    }
}
