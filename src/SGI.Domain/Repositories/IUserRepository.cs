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
        bool UserExists(int id);
        IEnumerable<User> GetAll();
        User GetById(int id);
        User Add(User newUser);
        void Update(User user);
        void Delete(int id);
    }
}
