namespace SGI.Domain.Repositories
{
    #region Using

    using System;
    using System.Collections.Generic;
    using Entities;
    using SGI.Domain.Helpers;

    #endregion

    public interface IUserRepository : IDisposable
    {
        IEnumerable<User> GetAll();
        User GetById(int id);
        User Add(User newUser);
        bool Update(User user);
        bool Delete(int id);
    }
}
