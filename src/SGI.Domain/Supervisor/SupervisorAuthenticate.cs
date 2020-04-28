namespace SGI.Domain.Supervisor
{
    #region Using

    using Domain.Models;
    using System.Threading;
    using System.Threading.Tasks;

    #endregion

    public partial class Supervisor
    {
        public async Task<UserViewModel> Login(string username, string password, CancellationToken ct = default)
        {
            var user = await _authenticateRepository.Login(username, password, ct);
            if (user == null)
                return null;

            var userViewModel = _mapper.Map<UserViewModel>(user);

            return userViewModel;
        }
    }
}
