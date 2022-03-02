using PupilRegister.Models.Entities;
using PupilRegister.Models.FormRequest;
using System.Threading.Tasks;

namespace PupilRegister.Interfaces
{
    public interface IUserService
    {
        Task<Parent> Create(RegisterRequest request, string password);
        Task<Parent> Authenticate(string username, string password);
        Task<Parent> GetById(int id);
    }
}
