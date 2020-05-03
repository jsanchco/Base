namespace SGI.Domain.Supervisor
{
    #region Using

    using SGI.Domain.Models;
    using System.Threading;
    using System.Threading.Tasks;

    #endregion

    public interface ISupervisor
    {
        #region Authenticate

        Task<UserViewModel> Login(string user, string password, CancellationToken ct = default);

        #endregion

        #region User

        bool UserExists(int id);
        Task<QueryResult<UserViewModel>> GetAllUsersAsync(int skip = 0, int take = 0, string orderBy= null, string filter = null);
        Task<UserViewModel> GetUserByIdAsync(int id);
        UserViewModel AddUser(UserViewModel newUserViewModel);
        void UpdateUser(UserViewModel userViewModel);
        void DeleteUser(int id);

        #endregion

        #region Role

        bool RoleExists(int id);
        Task <QueryResult<RoleViewModel>> GetAllRolesAsync(int skip, int take);
        Task<RoleViewModel> GetRoleByIdAsync(int id);
        RoleViewModel AddRole(RoleViewModel newRoleViewModel);
        void UpdateRole(RoleViewModel roleViewModel);
        void DeleteRole(int id);

        #endregion
    }
}
