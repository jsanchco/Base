namespace SGI.Domain.Supervisor
{
    #region Using

    using Models;
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
        QueryResult<UserViewModel> GetAllUsers(int skip = 0, int take = 0, string orderBy= null, string filter = null);
        UserViewModel GetUserById(int id);
        UserViewModel AddUser(UserViewModel newUserViewModel);
        void UpdateUser(UserViewModel userViewModel);
        void DeleteUser(int id);

        #endregion

        #region Role

        bool RoleExists(int id);
        QueryResult<RoleViewModel> GetAllRoles(int skip = 0, int take = 0);
        RoleViewModel GetRoleById(int id);
        RoleViewModel AddRole(RoleViewModel newRoleViewModel);
        void UpdateRole(RoleViewModel roleViewModel);
        void DeleteRole(int id);

        #endregion
    }
}
