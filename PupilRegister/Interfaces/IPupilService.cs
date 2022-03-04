using PupilRegister.Models.DTO;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PupilRegister.Interfaces
{
    public interface IPupilService
    {
        Task<List<PupilSchoolDto>> GetParentPupilSchools(int parentId);
    }
}
