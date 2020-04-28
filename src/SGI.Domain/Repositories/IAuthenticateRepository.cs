namespace SGI.Domain.Repositories
{  
    #region Using

    using System;
    using System.Threading;
    using System.Threading.Tasks;
    using Domain.Entities;

    #endregion

    public interface IAuthenticateRepository : IDisposable
    {
        Task<User> Login(string username, string password, CancellationToken ct = default);
    }
}
