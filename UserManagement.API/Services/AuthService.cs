using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using UserManagement.API.DTO;
using UserManagement.API.Entities;
using UserManagement.API.Helper;
using UserManagement.API.Repository;

namespace UserManagement.API.Services
{
    public class AuthService : IAuthService
    {
        private readonly IUserRepository _userRepository;
        private readonly IUserRoleRepository _userRoleRepository;
        private readonly IConfiguration _configuration;

        public AuthService(IUserRepository userRepository, IUserRoleRepository userRoleRepository, IConfiguration configuration)
        {
            _userRepository = userRepository;            
            _userRoleRepository = userRoleRepository;
            _configuration = configuration;
        }

        public async Task<string> RegisterAsync(RegisterDto dto)
        {
            User user = new User
            {
                Username = dto.Username,
                Password = dto.Password,
                Email = dto.Email,
                CreatedDate = DateTime.Now
            };

            var userEntity = await _userRepository.CreateUserAsync(user);

            // Assign default role (User) 
            var userRole = new UserRole
            {
                UserId = userEntity.Id,
                RoleId = (int)Role.User,
                CreatedAt = DateTime.Now
            };

            await _userRoleRepository.AddAsync(userRole);
            return "User registered successfully.";
        }

        public async Task<string> LoginAsync(string email, string password)
        {
            var user = await _userRepository.GetUsersByEmail(email);
            if (user == null || !user.Password.Equals(password))
            {
                throw new UnauthorizedAccessException("Invalid username or password.");
            }

            var token = GenerateJwtToken(user);

            return token;
        }

        public async Task<bool> IsEmailUniqueAsync(string email)
        {
            User usersWithEmail = await _userRepository.GetUsersByEmail(email);
            return usersWithEmail == null ? true : false;
        }

        public static List<KeyValuePair<int, Role>> GetAllRoles()
        {
            return Enum.GetValues(typeof(Role))
               .Cast<Role>()
               .Select(role => new KeyValuePair<int, Role>((int)role, role))
               .ToList();
        }

        public async Task<string> AssginUserRoleAsync(string email, int roleId)
        {
            User usersWithEmail = await _userRepository.GetUsersByEmail(email);
            if (usersWithEmail != null)
            {
                if (usersWithEmail.UserRoles == null || !usersWithEmail.UserRoles.Any(x => x.RoleId == roleId))
                {
                    KeyValuePair<int, Role> role = GetAllRoles().FirstOrDefault(x => x.Key == roleId);

                    if (role.Equals(default(KeyValuePair<int, Role>))) return "Current Role is not found";

                    UserRole userRole = new UserRole
                    {
                        UserId = usersWithEmail.Id,
                        RoleId = role.Key,
                        CreatedAt = DateTime.Now
                    };
                    await _userRoleRepository.AddAsync(userRole);
                }
                else
                {
                    return "User Role already exists.";
                }
            }

            return "User Role added successfully.";
        }

        private string GenerateJwtToken(User user)
        {
            var roles = _userRoleRepository.GetByIdAsync(user.Id).Result;

            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, user.Username),
                new Claim(ClaimTypes.Email, user.Email)
            };

            var allRoles = GetAllRoles();

            foreach (var role in roles)
            {
                claims.Add(new Claim(ClaimTypes.Role, allRoles.FirstOrDefault(x => x.Key == role.RoleId).Value.ToString() ?? null));
            }

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var minutes = Convert.ToInt32(_configuration["Jwt:ExpiryMinutes"]);

            var token = new JwtSecurityToken(
                issuer: _configuration["Jwt:Issuer"],
                audience: _configuration["Jwt:Audience"],
                claims: claims,
                expires: DateTime.Now.AddMinutes(minutes),
                signingCredentials: creds
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}


