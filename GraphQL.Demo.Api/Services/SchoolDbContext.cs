using GraphQL.Demo.Api.DTOs;
using Microsoft.EntityFrameworkCore;

namespace GraphQL.Demo.Api.Services
{
    public class SchoolDbContext : DbContext
    {
        public SchoolDbContext(DbContextOptions<SchoolDbContext> options) : base(options) { }

        public DbSet<CouseDTO> Couses { get; set; }
        public DbSet<InstructorDTO> Instructors { get; set; }
        public DbSet<StudentDTO> Students { get; set; }

    }
}
