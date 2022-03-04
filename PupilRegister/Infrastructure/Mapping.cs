using PupilRegister.Models.DTO;
using PupilRegister.Models.Entities;
using System.Collections.Generic;

namespace PupilRegister.Infrastructure
{
    public class Mapping
    {
        public List<PupilSchoolDto> MapPupilSchool(List<Pupil> pupilSchools)
        {
            var pupilSchoolsDto = new List<PupilSchoolDto>();

            foreach (var pupilSchool in pupilSchools)
            {
                var pupilSchoolDto = new PupilSchoolDto();
                pupilSchoolDto.Name = pupilSchool.Name;
                pupilSchoolDto.School = pupilSchool.School;
                pupilSchoolsDto.Add(pupilSchoolDto);
            }

            return pupilSchoolsDto; 
        }
    }
}
