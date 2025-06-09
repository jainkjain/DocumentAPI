using UserManagement.API.Entities;

namespace UserManagement.API.Repository
{
    /// <summary>
    /// Interface for user-role repository.
    /// Provides CRUD operations for UserRole entity.
    /// </summary>
    public interface IUserRoleRepository
    {
        /// <summary>
        /// Gets all user-role assignments.
        /// </summary>
        /// <returns>A collection of UserRole entities.</returns>
        Task<IEnumerable<UserRole>> GetAllAsync();

        /// <summary>
        /// Gets a specific UserRole based on user and role IDs.
        /// </summary>
        /// <param name="userId">The ID of the user.</param>
        /// <returns>A UserRole entity.</returns>
        Task<List<UserRole>> GetByIdAsync(Guid userId);

        /// <summary>
        /// Gets a specific UserRole based on user and role IDs.
        /// </summary>
        /// <param name="userId">The ID of the user.</param>
        /// <param name="roleId">The ID of the role.</param>
        /// <returns>A UserRole entity.</returns>
        Task<UserRole> GetByIdAsync(Guid userId, int roleId);

        /// <summary>
        /// Adds a new UserRole assignment.
        /// </summary>
        /// <param name="userRole">The UserRole entity to add.</param>
        Task AddAsync(UserRole userRole);

        /// <summary>
        /// Updates an existing UserRole assignment.
        /// </summary>
        /// <param name="userRole">The UserRole entity to update.</param>
        Task UpdateAsync(UserRole userRole);

        /// <summary>
        /// Deletes a UserRole assignment.
        /// </summary>
        /// <param name="userId">The ID of the user.</param>
        /// <param name="roleId">The ID of the role.</param>
        Task DeleteAsync(Guid userId, int roleId);
    }
}
