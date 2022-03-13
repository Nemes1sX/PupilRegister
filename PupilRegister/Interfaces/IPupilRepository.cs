using PupilRegister.Models.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PupilRegister.Interfaces
{
    public interface IPupilRepository
    {
        Task<List<Pupil>> GetPupilSchool(int parentId);
    }
}
