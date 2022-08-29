using NUnit.Framework;
using PupilRegister.Models.Entities;
using System.Threading.Tasks;

namespace PupilRegisterTest
{
    public class PupilTest : IntegrationTestBase
    {
        [Test]
        public async Task AddParent()
        {
            var parent = new Parent {
                Name = "John Doe",
                Email = "johndoe@test.com",              
            };

            dbContext.Parents.Add(parent);
            await dbContext.SaveChangesAsync();

            var parentFromDb = dbContext.Parents.Find(parent.Id);
            Assert.AreEqual(parent.Name, parentFromDb.Name);
            Assert.AreEqual(parent.Email, parentFromDb.Email);
        }
    }
}