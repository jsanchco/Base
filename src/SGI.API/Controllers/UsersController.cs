namespace SGI.API.Controllers
{
    #region Using

    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.Logging;
    using SGI.Domain.Helpers;
    using SGI.Domain.Models;
    using SGI.Domain.Supervisor;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Microsoft.AspNetCore.Authorization;

    #endregion

    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly ISupervisor _supervisor;
        private readonly ILogger<UsersController> _logger;

        public UsersController(ILogger<UsersController> logger, ISupervisor supervisor)
        {
            _logger = logger ??
                throw new ArgumentNullException(nameof(logger));

            _supervisor = supervisor ?? 
                throw new ArgumentNullException(nameof(supervisor));
        }

        // GET: api/Users
        [HttpGet]
        public ActionResult<IEnumerable<UserViewModel>> GetUsers()
        {
            try
            {
                //throw new Exception("Custom exception");
                var queryString = Request.Query;
                var skip = Convert.ToInt32(queryString["$skip"]);
                var take = Convert.ToInt32(queryString["$top"]);
                var orderBy = Convert.ToString(queryString["$orderby"]);
                var filter = ((string)queryString["$filter"]).GetSearcher();

                var result = _supervisor.GetAllUsers(skip, take, orderBy, filter);
                return Ok(new { Items = result.ToList(), Count = result.Count() });
            }
            catch(Exception ex)
            {
                _logger.LogError(ex, "Exception: ");
                return StatusCode(500, ex.Message);
            }
        }

        // GET api/Users/5
        [HttpGet("{userId}", Name = "GetUserById")]
        public ActionResult GetUserById(int userId)
        {
            try
            {
                if (!_supervisor.UserExists(userId))
                {
                    return NotFound();
                }

                return Ok(_supervisor.GetUserById(userId));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Exception: ");
                return StatusCode(500, ex.Message);
            }
        }

        [HttpPost]
        public ActionResult AddUser([FromBody]UserViewModel userViewModel)
        {
            try
            {
                var result = _supervisor.AddUser(userViewModel);

                return CreatedAtRoute("GetUserById", 
                    new { userId = result.id },
                    result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Exception: ");
                return StatusCode(500, ex.Message);
            }
        }

        [HttpPut]
        public ActionResult UpdateUser([FromBody]UserViewModel userViewModel)
        {
            try
            {
                if (userViewModel == null)
                {
                    throw new ArgumentNullException(nameof(userViewModel));
                }

                if (!_supervisor.UserExists(userViewModel.id))
                {
                    return NotFound();
                }

                _supervisor.UpdateUser(userViewModel);

                return CreatedAtRoute("GetUserById",
                    new { userId = userViewModel.id },
                    userViewModel);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Exception: ");
                return StatusCode(500, ex.Message);
            }
        }

        // DELETE: api/users/5
        [HttpDelete("{userId:int}")]
        //[Route("users/{userId:int}")]
        public ActionResult Delete(int userId)
        {
            try
            {
                if (!_supervisor.UserExists(userId))
                {
                    return NotFound();
                }

                _supervisor.DeleteUser(userId);

                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Exception: ");
                return StatusCode(500, ex.Message);
            }
        }
    }
}
