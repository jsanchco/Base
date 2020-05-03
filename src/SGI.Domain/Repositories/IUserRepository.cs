namespace SGI.Domain.Repositories
{
    #region Using

    using Entities;
    using System;
    using System.Linq;
    using System.Threading.Tasks;

    #endregion

    public interface IUserRepository : IDisposable
    {
        bool UserExists(int id);
        User GetById(int id);
        IQueryable<User> GetAll();
        Task<User> GetByIdAsync(int id);
        User Add(User newUser);
        void Update(User user);
        void Delete(int id);
    }
}
