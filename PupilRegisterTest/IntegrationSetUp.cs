using NUnit.Framework;
using PupilRegister.DataContext;
using System.Threading.Tasks;

namespace PupilRegisterTest
{
    [SetUpFixture]
    public class IntegrationSetUp
    {
        [OneTimeSetUp]
        public async Task SetUp()
        {
            var dbContextFactory = new SampleDbContextFactory();
            using (var dbContext = dbContextFactory.CreateDbContext(new string [] {}))
            {
                await dbContext.Database.EnsureDeletedAsync();
                await dbContext.Database.EnsureCreatedAsync();
            }
        }
    }
}
