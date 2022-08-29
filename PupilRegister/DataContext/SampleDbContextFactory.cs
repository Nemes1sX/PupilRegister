using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace PupilRegister.DataContext
{
    public class SampleDbContextFactory : IDesignTimeDbContextFactory<PupilRegisterContext>
    {
        public PupilRegisterContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<PupilRegisterContext>();
            optionsBuilder.UseSqlServer(
              "Server=(localdb)\\mssqllocaldb;Database=PupilRegister.Testing;Trusted_Connection=True;");

            return new PupilRegisterContext(optionsBuilder.Options);
        }
    }
}
