using Microsoft.EntityFrameworkCore;
using PupilRegister.DataContext;
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
        private readonly PupilRegisterContext _db;
        private readonly Mapping _mapper;

        public PupilService(PupilRegisterContext db, Mapping mapper)
        {
            _db = db;
            _mapper = mapper;
        }

        public async Task<List<PupilSchoolDto>> GetParentPupilSchools(int parentId)
        {
            var pupilSchools = await _db.Pupils.Where(x => x.ParentId == parentId).Include(x => x.School).ToListAsync();

            if (!pupilSchools.Any())
                return null;

            return _mapper.MapPupilSchool(pupilSchools);
        }
    }
}
