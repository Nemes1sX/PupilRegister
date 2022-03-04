using Microsoft.EntityFrameworkCore;
using PupilRegister.Configuration;
using PupilRegister.DataContext;
using PupilRegister.Interfaces;
using PupilRegister.Models.Entities;
using PupilRegister.Models.FormRequest;
using System;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace PupilRegister.Services
{
    public class UserService : IUserService
    {

        private readonly PupilRegisterContext _db;

        public UserService(PupilRegisterContext db)
        {
            _db = db;
        }

        public async Task<Parent> Authenticate(string username, string password)
        {
            if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
                return null;

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
            // validation
            if (string.IsNullOrWhiteSpace(password))
                throw new AppException("Password is required");

            if (_db.Parents.Any(x => x.Email == request.Email))
                throw new AppException("Username \"" + request.Email + "\" is already taken");

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

        public async Task<Parent> GetById(int id)
        {
            var parent = await _db.Parents.FindAsync(id);
            if (parent == null)
                throw new AppException("Parent isn't existed");

            return parent;
        }

    }
}
