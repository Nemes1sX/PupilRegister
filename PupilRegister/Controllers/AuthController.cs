using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using PupilRegister.Configuration;
using PupilRegister.Interfaces;
using PupilRegister.Models.FormRequest;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.Extensions.Options;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using PupilRegister.Models.Entities;
using PupilRegister.DataContext;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace PupilRegister.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {

        private readonly IUserService _userService;
        private readonly PupilRegisterContext _db;


        public AuthController(IUserService userService,  PupilRegisterContext db)
        {
            _userService = userService;
            _db = db;
        }


        [HttpPost]
        [Route("login")]
        public async Task<IActionResult> Authenticate([FromBody] LoginRequest request)
        {
            var parent = await _userService.Authenticate(request.Email, request.Password);

            if (parent == null)
                return BadRequest(new { message = "Username or password is incorrect" });

            var tokenString = _userService.GenerateToken(parent.Id);

            // return basic user info and authentication token
            return Ok(new
            {
                Token = tokenString,         
            });
        }

   
        [HttpPost]
        [Route("register")]
        public IActionResult Register([FromBody] RegisterRequest request)
        {
            _userService.Create(request, request.Password);
            return Ok(new {Name =  request.Name, Email = request.Email});
        }
    }
}
