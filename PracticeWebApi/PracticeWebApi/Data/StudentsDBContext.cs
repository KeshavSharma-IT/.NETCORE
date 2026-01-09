using Microsoft.EntityFrameworkCore;
using PracticeWebApi.Models;

namespace PracticeWebApi.Data
{
    public class StudentsDBContext       : DbContext
    {
        public StudentsDBContext(DbContextOptions<StudentsDBContext> options) : base(options) { }

        public DbSet<Student> Students { get; set; }

    }
}
