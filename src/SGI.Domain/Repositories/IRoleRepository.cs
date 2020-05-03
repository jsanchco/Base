namespace SGI.Domain.Repositories
{
    #region Using

    using SGI.Domain.Entities;
    using SGI.Domain.Models;
    using System;
    using System.Linq;
    using System.Threading.Tasks;

    #endregion

    public interface IRoleRepository : IDisposable
    {
        bool RoleExists(int id);
        IQueryable<Role> GetAll();
        Role GetById(int id);
        Task<Role> GetByIdAsync(int id);
        Task<TransactionResult<Role>> AddAsync(Role newRole);
        Task<bool> UpdateAsync(Role role);
        Task<bool> DeleteAsync(int id);
    }
}
