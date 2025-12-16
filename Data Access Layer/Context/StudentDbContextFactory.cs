using Microsoft.EntityFrameworkCore.Design;
using Microsoft.EntityFrameworkCore;

namespace Backend.Data_Access_Layer.Context
{
    public class StudentDbContextFactory : IDesignTimeDbContextFactory<StudentDbContext>
    {
        public StudentDbContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<StudentDbContext>();

            // Use the same connection string as in appsettings.json
            optionsBuilder.UseSqlServer("Server=PRUDHVI\\SQLEXPRESS;Database=StudentTable;Trusted_Connection=True;TrustServerCertificate=True");

            return new StudentDbContext(optionsBuilder.Options);
        }
    }
}
