namespace SGI.DataEFCoreSQL.Repositories
{
    #region Using

    using Domain.Entities;
    using Microsoft.EntityFrameworkCore;
    using SGI.Domain.Repositories;
    using System;
    using System.Threading;
    using System.Threading.Tasks;

    #endregion

    public class AuthenticateRepository : IAuthenticateRepository
    {
        private readonly EFContextSQL _context;

        public AuthenticateRepository(EFContextSQL context)
        {
            _context = context;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                _context.Dispose();
            }
        }

        public async Task<User> Login(string username, string password, CancellationToken ct = default)
        {
            return await _context.Users
                .Include(x => x.Role)
                .FirstOrDefaultAsync(x => x.Username == username && x.Password == password, ct);
        }
    }
}
