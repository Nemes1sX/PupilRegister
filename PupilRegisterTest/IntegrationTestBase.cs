using NUnit.Framework;
using PupilRegister.DataContext;

namespace PupilRegisterTest
{
    public abstract class IntegrationTestBase
    {
        protected PupilRegisterContext dbContext = null;

        [SetUp]
        public void SetUp()
        {
            var dbContextFactory = new SampleDbContextFactory();
            dbContext = dbContextFactory.CreateDbContext(new string[] { });
        }

        [TearDown]
        public void TearDown()
        {
            if (dbContext != null)
            {
                dbContext.Dispose();
            }
        }
    }
}
