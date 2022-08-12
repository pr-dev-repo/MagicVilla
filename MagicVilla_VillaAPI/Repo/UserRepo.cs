using MagicVilla_VillaAPI.Data;
using MagicVilla_VillaAPI.Models;
using MagicVilla_VillaAPI.Models.Dto;
using MagicVilla_VillaAPI.Repo.IRepo;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace MagicVilla_VillaAPI.Repo
{
    public class UserRepo : IUserRepo
    {
        private readonly ApplicationDbContext _db;
        private string secretKey;
        public UserRepo(ApplicationDbContext db, IConfiguration config)
        {
            _db = db;
            secretKey = config.GetValue<string>("ApiSettings:Secret");
        }

        public bool IsUniqueUser(string username)
        {
            var user = _db.LocalUsers.FirstOrDefault(x => x.Username == username);
            return user == null;
        }

        public async Task<LoginResponseDTO> Login(LoginRequestDTO request)
        {
            var user = _db.LocalUsers.FirstOrDefault(x => x.Username == request.Username && x.Password == request.Password);

            if (user == null)
                return new LoginResponseDTO()
                {
                    Token = string.Empty,
                    User = null
                };

            // JWT
            var tokenHanlder = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(secretKey); // encode

            // token descriptor
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Name, user.Id.ToString()),
                    new Claim(ClaimTypes.Role, user.Role),
                }),
                Expires = DateTime.UtcNow.AddDays(7),
                SigningCredentials = new(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHanlder.CreateToken(tokenDescriptor); // creates token

            return new LoginResponseDTO()
            {
                Token = tokenHanlder.WriteToken(token), // write, deserialize
                User = user,

            };
        }

        public async Task<LocalUSer> Register(RegistrationRequestDTO request)
        {
            LocalUSer user = new()
            {
                Username = request.Username,
                Password = request.Password,
                Name = request.Name,
                Role = request.Role
            };
            _db.LocalUsers.Add(user);
            await _db.SaveChangesAsync();
            user.Password = string.Empty;
            return user;
        }
    }
}
