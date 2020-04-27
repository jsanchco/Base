namespace SGI.Domain.Repositories
{
    #region Using

    using SGI.Domain.Entities;
    using System;
    using System.Collections.Generic;

    #endregion

    public interface IRoleRepository : IDisposable
    {
        List<Role> GetAll();
        Role GetById(int id);
        Role Add(Role newRole);
        bool Update(Role role);
        bool Delete(int id);
    }
}
