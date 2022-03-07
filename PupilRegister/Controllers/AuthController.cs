﻿using Microsoft.AspNetCore.Authorization;
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
        //private readonly UserManager<Parent> _userManager;
        private readonly PupilRegisterContext _db;
        private readonly JwtConfig _jwtConfig;

        public AuthController(IUserService userService, IOptions<JwtConfig> jwtConfig, PupilRegisterContext db)
        {
            _userService = userService;
            _db = db;
            //_userManager = userManager;
            _jwtConfig = jwtConfig.Value;
        }


        [HttpPost]
        [Route("login")]
        public async Task<IActionResult> Authenticate([FromBody] LoginRequest request)
        {
            var parent = await _userService.Authenticate(request.Email, request.Password);

            if (parent == null)
                return BadRequest(new { message = "Username or password is incorrect" });

            //var currentParent = await _db.Parents.SingleOrDefaultAsync(x => x.Email == request.Email);
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.UTF8.GetBytes(_jwtConfig.Secret);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Name, parent.Id.ToString())
                }),
                Expires = DateTime.UtcNow.AddDays(7),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            var tokenString = tokenHandler.WriteToken(token);

            // return basic user info and authentication token
            return Ok(new
            {
                Token = tokenString,         
            });
        }

   
        [HttpPost]
        public IActionResult Register([FromBody] RegisterRequest request)
        {

            try
            {
                // create user
                _userService.Create(request, request.Password);
                return Ok();
            }
            catch (AppException ex)
            {
                // return error message if there was an exception
                return BadRequest(new { message = ex.Message });
            }
        }
    }
}
