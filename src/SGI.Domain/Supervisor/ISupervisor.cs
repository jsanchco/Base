namespace SGI.Domain.Supervisor
{
    #region Using

    using SGI.Domain.Models;
    using System.Collections;
    using System.Collections.Generic;
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
        UserViewModel GetUserById(int id);
        UserPatch GetUserPatchById(int id);
        Task<QueryResult<UserViewModel>> GetAllUsersAsync(int skip = 0, int take = 0, string orderBy= null, string filter = null);
        Task<QueryResult<UserViewModel>> GetUsersByRolesAsync(IEnumerable<int> ids);
        Task<UserViewModel> GetUserByIdAsync(int id);
        Task<TransactionResult<UserViewModel>> AddUserAsync(UserViewModel newUserViewModel);
        Task<bool> UpdateUserAsync(UserViewModel userViewModel);
        Task<bool> DeleteUserAsync(int id);

        #endregion

        #region Role

        bool RoleExists(int id);
        Task <QueryResult<RoleViewModel>> GetAllRolesAsync(int skip, int take);
        Task<RoleViewModel> GetRoleByIdAsync(int id);
        Task<TransactionResult<RoleViewModel>> AddRoleAsync(RoleViewModel newRoleViewModel);
        Task<bool> UpdateRoleAsync(RoleViewModel roleViewModel);
        Task<bool> DeleteRoleAsync(int id);

        #endregion
    }
}
