namespace SGI.API.Controllers
{
    #region Using

    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.Logging;
    using SGI.Domain.Helpers;
    using SGI.Domain.Models;
    using SGI.Domain.Supervisor;
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;

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

        [HttpGet]
        public async Task<ActionResult<IEnumerable<UserViewModel>>> GetUsers()
        {
            try
            {
                var queryString = Request.Query;
                var skip = Convert.ToInt32(queryString["$skip"]);
                var take = Convert.ToInt32(queryString["$top"]);
                var orderBy = Convert.ToString(queryString["$orderby"]);
                var filter = ((string)queryString["$filter"]).GetSearcher();

                return Ok(await _supervisor.GetAllUsersAsync(skip, take, orderBy, filter));
            }
            catch(Exception ex)
            {
                _logger.LogError(ex, "Exception: ");
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpGet("{userId}", Name = "GetUserById")]
        public async Task<ActionResult> GetUserById(int userId)
        {
            try
            {
                if (!_supervisor.UserExists(userId))
                {
                    return NotFound();
                }

                return Ok(await _supervisor.GetUserByIdAsync(userId));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Exception: ");
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpPost]
        public async Task<ActionResult<UserViewModel>> AddUser(UserViewModel userViewModel)
        {
            try
            {
                var result = await _supervisor.AddUserAsync(userViewModel);
                if (!result.Result || result.Item == null)
                    return StatusCode(StatusCodes.Status500InternalServerError, "Error in create User");

                return CreatedAtRoute("GetUserById",
                    new { userId = result.Item.id },
                    result.Item);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Exception: ");
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpPut]
        public async Task<ActionResult> UpdateUser(int? userId, UserViewModel userViewModel)
        {
            try
            {
                if (userViewModel == null)
                {
                    throw new ArgumentNullException(nameof(userViewModel));
                }

                if (userId == null)
                    userId = userViewModel.id;

                if (!_supervisor.UserExists((int)userId))
                {
                    return NotFound();
                }

                if (!await _supervisor.UpdateUserAsync(userViewModel))
                {
                    return StatusCode(StatusCodes.Status500InternalServerError, "Error in update User");
                }

                return CreatedAtRoute("GetUserById",
                    new { userId = userViewModel.id },
                    userViewModel);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Exception: ");
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpDelete("{userId:int}")]
        public async Task<ActionResult> Delete(int userId)
        {
            try
            {
                if (!_supervisor.UserExists(userId))
                {
                    return NotFound();
                }

                if (!await _supervisor.DeleteUserAsync(userId))
                {
                    return StatusCode(StatusCodes.Status500InternalServerError, "Error in remove User");
                }

                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Exception: ");
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }
    }
}
