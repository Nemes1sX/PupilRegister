using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PupilRegister.Services;
using System.Threading.Tasks;

namespace PupilRegister.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SchoolController : ControllerBase
    {
        private readonly PupilService _pupilService;

        public SchoolController(PupilService pupilService)
        {
            _pupilService = pupilService;
        }

        [HttpGet]
        [Route("pupil")]
        public async Task<IActionResult> PupilSchools(int parentId)
        {
            return Ok(new { PupilSchools = await _pupilService.GetParentPupilSchools(parentId) });
        }
    }
}
