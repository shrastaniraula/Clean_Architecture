using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.StudentCRUD;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.StudentCRUD
{
    public class ApplicationDBContext: IdentityDbContext<AppUser>
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server=DESKTOP-DTHIE3G\\SQLEXPRESS; Database = CleanStudentData; Trusted_Connection=True; TrustServerCertificate=True; MultipleActiveResultSets=True");
        }

        public DbSet<Student> Students { get; set; }
    }
}
