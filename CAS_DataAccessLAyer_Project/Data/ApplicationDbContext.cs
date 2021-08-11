
using CAS_DataAccessLAyer_Project.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace CAS_DataAccessLayer.Data
{

    public class context : DbContext
    {
        public context(DbContextOptions<context> options)
            : base(options)
        {
        }

        public DbSet<Student> Student { get; set; }
        public DbSet<UniStudent> uniStudents { get; set; }
        public DbSet<Course> courses { get; set; }
        public DbSet<Department> departments { get; set; }
    }
}
