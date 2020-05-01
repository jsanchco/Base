namespace SGI.Domain.Repositories
{
    #region Using

    using SGI.Domain.Entities;
    using System;
    using System.Collections.Generic;

    #endregion

    public interface IRoleRepository : IDisposable
    {
        bool RoleExists(int id);
        IEnumerable<Role> GetAll();
        Role GetById(int id);
        Role Add(Role newRole);
        void Update(Role role);
        void Delete(int id);
    }
}
