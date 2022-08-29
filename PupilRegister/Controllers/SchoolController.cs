using Microsoft.AspNetCore.Authorization;
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

        [Authorize]
        [HttpGet]
        [Route("pupil")]
        public async Task<IActionResult> PupilSchools()
        {
            int userId;
            int.TryParse(User.Identity.Name, out userId);

            var pupilSchools = await _pupilService.GetParentPupilSchools(userId);

            if (pupilSchools == null)
                return NotFound();

            return Ok(new { PupilSchools = pupilSchools });
        }
    }
}
