using Entities.Models;

namespace Repositories.Contracts
{
    public interface IClubRoleDefinitionRepository
    {
        Task<IEnumerable<ClubRoleDefinition>> GetAllAsync(bool trackChanges);
        Task<ClubRoleDefinition?> GetByRoleValueAsync(int roleValue, bool trackChanges);
    }
}
