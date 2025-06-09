using Microsoft.EntityFrameworkCore;
using UserManagement.API.Context;
using UserManagement.API.Entities;

namespace UserManagement.API.Repository
{
    /// <summary>
    /// Repository for performing CRUD operations on UserRole entities.
    /// </summary>
    public class UserRoleRepository : IUserRoleRepository
    {
        private readonly ApplicationDbContext _context;

        /// <summary>
        /// Initializes a new instance of the UserRoleRepository class.
        /// </summary>
        /// <param name="context">Database context to be used by the repository.</param>
        public UserRoleRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Retrieves all UserRole records from the database.
        /// </summary>
        /// <returns>A list of UserRole entities.</returns>
        public async Task<IEnumerable<UserRole>> GetAllAsync()
        {
            return await _context.UserRoles
                .Include(ur => ur.User)
                .ToListAsync();
        }

        /// <summary>
        /// Retrieves a List of UserRole record by user ID and role ID.
        /// </summary>
        /// <param name="userId">User ID of the UserRole.</param>
        /// <returns>A UserRole entity if found; otherwise, null.</returns>
        public async Task<List<UserRole>> GetByIdAsync(Guid userId)
        {
            return await _context.UserRoles
                .Include(ur => ur.User)
                .Where(ur => ur.UserId == userId).ToListAsync();
        }


        /// <summary>
        /// Retrieves a UserRole record by user ID and role ID.
        /// </summary>
        /// <param name="userId">User ID of the UserRole.</param>
        /// <param name="roleId">Role ID of the UserRole.</param>
        /// <returns>A UserRole entity if found; otherwise, null.</returns>
        public async Task<UserRole> GetByIdAsync(Guid userId, int roleId)
        {
            return await _context.UserRoles
                .Include(ur => ur.User)
                .FirstOrDefaultAsync(ur => ur.UserId == userId && ur.RoleId == roleId);
        }

        /// <summary>
        /// Adds a new UserRole to the database.
        /// </summary>
        /// <param name="userRole">The UserRole entity to be added.</param>
        public async Task AddAsync(UserRole userRole)
        {
            await _context.UserRoles.AddAsync(userRole);
            await _context.SaveChangesAsync();
        }

        /// <summary>
        /// Updates an existing UserRole in the database.
        /// </summary>
        /// <param name="userRole">The UserRole entity to be updated.</param>
        public async Task UpdateAsync(UserRole userRole)
        {
            _context.UserRoles.Update(userRole);
            await _context.SaveChangesAsync();
        }

        /// <summary>
        /// Deletes a UserRole from the database.
        /// </summary>
        /// <param name="userId">The user ID associated with the UserRole.</param>
        /// <param name="roleId">The role ID associated with the UserRole.</param>
        public async Task DeleteAsync(Guid userId, int roleId)
        {
            var userRole = await GetByIdAsync(userId, roleId);
            if (userRole != null)
            {
                _context.UserRoles.Remove(userRole);
                await _context.SaveChangesAsync();
            }
        }
    }
}
