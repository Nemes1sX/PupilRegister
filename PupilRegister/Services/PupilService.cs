using Microsoft.EntityFrameworkCore;
using PupilRegister.Infrastructures;
using PupilRegister.Interfaces;
using PupilRegister.Models.DTO;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PupilRegister.Services
{
    public class PupilService : IPupilService
    {
        private readonly IPupilRepository _repository;
        private readonly Mapping _mapper;

        public PupilService(Mapping mapper, IPupilRepository repository)
        {
            _mapper = mapper;
            _repository = repository;
        }

        public async Task<List<PupilSchoolDto>> GetParentPupilSchools(int parentId)
        {
            var pupilSchools = await _repository.GetPupilSchool(parentId);

            if (!pupilSchools.Any())
                return null;

            return _mapper.MapPupilSchool(pupilSchools);
        }
    }
}
