using PupilRegister.Models.Entities;
using System.Threading.Tasks;

namespace PupilRegister.Interfaces
{
    public interface IUserService
    {
        Task<Parent> Create(Parent parent, string password);
        Task<Parent> Authenticate (string username, string password)
    }
}
