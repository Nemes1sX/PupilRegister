using PupilRegister.Models.Entities;
using System.Collections.Generic;

namespace PupilRegister.Models.DTO
{
    public class PupilSchoolDto
    {
        public string Name { get; set; }
        public List<School> Schools { get; set; }
    }
}
