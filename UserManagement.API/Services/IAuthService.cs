using UserManagement.API.DTO;

namespace UserManagement.API.Services
{
    public interface IAuthService
    {
        Task<string> RegisterAsync(RegisterDto dto);

        Task<string> LoginAsync(string email, string password);

        Task<bool> IsEmailUniqueAsync(string email);

        Task<string> AssginUserRoleAsync(string email, int roleId);
    }
}
