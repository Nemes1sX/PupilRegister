using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using PupilRegister.Configuration;
using PupilRegister.DataContext;
using PupilRegister.Interfaces;
using PupilRegister.Models.Entities;
using PupilRegister.Models.FormRequest;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace PupilRegister.Services
{
    public class UserService : IUserService
    {

        private readonly PupilRegisterContext _db;
        private readonly JwtConfig _jwtConfig;

        public UserService(PupilRegisterContext db, IOptions<JwtConfig> jwtConfig)
        {
            _db = db;
            _jwtConfig = jwtConfig.Value;
        }

        public async Task<Parent> Authenticate(string username, string password)
        {  
            var user = await _db.Parents.SingleOrDefaultAsync(x => x.Email == username);

            // check if username exists
            if (user == null)
                return null;

            // check if password is correct
            if (!PasswordHash.VerifyPasswordHash(password, user.PasswordHash, user.PasswordSalt))
                return null;

            // authentication successful
            return user;
        }

        public async Task<Parent> Create(RegisterRequest request, string password)
        {
            byte[] passwordHash, passwordSalt;
           PasswordHash.CreatePasswordHash(password, out passwordHash, out passwordSalt);


            var parent = new Parent();
            parent.Email = request.Email;
            parent.Name = request.Name;
            parent.PasswordHash = passwordHash;
            parent.PasswordSalt = passwordSalt;

            _db.Parents.Add(parent);
            await _db.SaveChangesAsync();

            return parent;
        }

        public string GenerateToken(int parentId)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.UTF8.GetBytes(_jwtConfig.Secret);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Name, parentId.ToString())
                }),
                Expires = DateTime.UtcNow.AddDays(7),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            var tokenString = tokenHandler.WriteToken(token);

            return tokenString;
        }

        public async Task<Parent> GetById(int id)
        {
            var parent = await _db.Parents.FindAsync(id);
            if (parent == null)
                throw new AppException("Parent isn't existed");

            return parent;
        }

    }
}
