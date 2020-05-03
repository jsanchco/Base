namespace SGI.Domain.Repositories
{
    #region Using

    using SGI.Domain.Entities;
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
        Role Add(Role newRole);
        void Update(Role role);
        void Delete(int id);
    }
}
