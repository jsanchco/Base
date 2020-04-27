namespace SGI.Domain.Supervisor
{   
    #region Using

    using System.Collections.Generic;
    using Domain.Helpers;
    using SGI.Domain.Models;

    #endregion

    public interface ISupervisor
    {
        #region User

        QueryResult<UserViewModel> GetAllUsers(int skip = 0, int take = 0, string orderBy= null, string filter = null, List<int> roles = null);
        UserViewModel GetUserById(int id);
        UserViewModel AddUser(UserViewModel newUserViewModel);
        bool UpdateUser(UserViewModel userViewModel);
        bool DeleteUser(int id);

        #endregion

        #region Role

        List<RoleViewModel> GetAllRole();
        RoleViewModel GetRoleById(int id);
        RoleViewModel AddRole(RoleViewModel newRoleViewModel);
        bool UpdateRole(RoleViewModel roleViewModel);
        bool DeleteRole(int id);

        #endregion
    }
}
