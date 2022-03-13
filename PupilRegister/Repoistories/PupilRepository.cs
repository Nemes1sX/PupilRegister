using Microsoft.EntityFrameworkCore;
using PupilRegister.DataContext;
using PupilRegister.Interfaces;
using PupilRegister.Models.Entities;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PupilRegister.Repoistories
{
    public class PupilRepository : IPupilRepository
    {

        private readonly PupilRegisterContext _db;

        public PupilRepository(PupilRegisterContext db)
        {
            _db = db;
        }

        public async Task<List<Pupil>> GetPupilSchool(int parentId)
        {
            return await _db.Pupils.Where(x => x.ParentId == parentId).Include(x => x.School).ToListAsync();
        }
    }
}
