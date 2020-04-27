namespace SGI.API.Controllers
{
    #region Using

    using Microsoft.AspNetCore.Mvc;
    using SGI.Domain.Models;
    using SGI.Domain.Supervisor;
    using System;
    using System.Collections.Generic;

    #endregion

    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly ISupervisor _supervisor;

        public UsersController(ISupervisor supervisor)
        {
            _supervisor = supervisor ?? 
                throw new ArgumentNullException(nameof(supervisor));
        }

        // GET: api/Users
        [HttpGet]
        public ActionResult<IEnumerable<UserViewModel>> GetUsers()
        {
            var result = _supervisor.GetAllUsers();
            return Ok(result);
        }

        //[HttpGet]
        //public object GetUsers()
        //{
        //    var result = _supervisor.GetAllUsers();
        //    return new { Items = result.Items, Count = result.Count };
        //}
    }
}
