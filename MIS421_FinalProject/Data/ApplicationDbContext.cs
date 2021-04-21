using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using MIS421_FinalProject.Models;

namespace MIS421_FinalProject.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        public DbSet<MIS421_FinalProject.Models.Department> Department { get; set; }
        public DbSet<MIS421_FinalProject.Models.Employee> Employee { get; set; }
        public DbSet<MIS421_FinalProject.Models.Document> Document { get; set; }
    }
}
