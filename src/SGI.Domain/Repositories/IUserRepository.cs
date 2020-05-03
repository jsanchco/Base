namespace SGI.Domain.Repositories
{
    #region Using

    using Entities;
    using SGI.Domain.Models;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    #endregion

    public interface IUserRepository : IDisposable
    {
        bool UserExists(int id);
        User GetById(int id);
        IQueryable<User> GetAll();
        IQueryable<User> GetByRoles(IEnumerable<int> ids);
        Task<User> GetByIdAsync(int id);
        Task<TransactionResult<User>> AddAsync(User newUser);
        Task<bool> UpdateAsync(User user);
        Task<bool> DeleteAsync(int id);
    }
}
