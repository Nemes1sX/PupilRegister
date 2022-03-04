using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PupilRegister.Interfaces;
using System.Threading.Tasks;

namespace PupilRegister.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SchoolController : ControllerBase
    {
        private readonly IPupilService _pupilService;

        public SchoolController(IPupilService pupilService)
        {
            _pupilService = pupilService;
        }

        [HttpGet]
        [Route("pupil")]
        public async Task<IActionResult> PupilSchools(int id = 1)
        {
            var pupilSchools = await _pupilService.GetParentPupilSchools(id);

            if (pupilSchools == null)
                return NotFound();

            return Ok(new { PupilSchools = pupilSchools });
        }
    }
}
