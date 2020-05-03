namespace SGI.API.Controllers
{
    #region Using

    using Domain.Models;
    using Domain.Supervisor;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.Logging;
    using SGI.API.Helpers;
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    #endregion

    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class RolesController : ControllerBase
    {
        private readonly ISupervisor _supervisor;
        private readonly ILogger<RolesController> _logger;

        public RolesController(ILogger<RolesController> logger, ISupervisor supervisor)
        {
            _logger = logger ??
                throw new ArgumentNullException(nameof(logger));

            _supervisor = supervisor ??
                throw new ArgumentNullException(nameof(supervisor));
        }

        // GET: api/Roles
        [HttpGet]
        public async Task<ActionResult<QueryResult<RoleViewModel>>> GetRoles()
        {
            try
            {
                var queryString = Request.Query;
                var skip = Convert.ToInt32(queryString["$skip"]);
                var take = Convert.ToInt32(queryString["$top"]);

                return Ok(await _supervisor.GetAllRolesAsync(skip, take));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Exception: ");
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpGet]
        [Route("{ids}/users")]
        public async Task<ActionResult<QueryResult<RoleViewModel>>> GetUsersByRole([FromRoute]
        [ModelBinder(BinderType = typeof(ArrayModelBinder))] IEnumerable<int> ids)
        {
            try
            {
                return Ok(await _supervisor.GetUsersByRolesAsync(ids));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Exception: ");
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        // GET api/Roles/5
        [HttpGet("{roleId}", Name = "GetRoleById")]
        public async Task<ActionResult> GetRoleById(int roleId)
        {
            try
            {
                if (!_supervisor.RoleExists(roleId))
                {
                    return NotFound();
                }

                return Ok(await _supervisor.GetRoleByIdAsync(roleId));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Exception: ");
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpPost]
        public async Task<ActionResult<RoleViewModel>> AddRole(RoleViewModel roleViewModel)
        {
            try
            {
                var result = await _supervisor.AddRoleAsync(roleViewModel);
                if (!result.Result || result.Item == null)
                    return StatusCode(StatusCodes.Status500InternalServerError, "Error in create Role");

                return CreatedAtRoute("GetRoleById",
                    new { roleId = result.Item.id },
                    result.Item);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Exception: ");
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpPut]
        public async Task<ActionResult> UpdateRole(int? roleId, RoleViewModel roleViewModel)
        {
            try
            {
                if (roleViewModel == null)
                {
                    throw new ArgumentNullException(nameof(roleViewModel));
                }

                if (roleId == null)
                    roleId = roleViewModel.id;

                if (!_supervisor.RoleExists((int)roleId))
                {
                    return NotFound();
                }

                if (!await _supervisor.UpdateRoleAsync(roleViewModel))
                {
                    return StatusCode(StatusCodes.Status500InternalServerError, "Error in update Role");
                }

                return CreatedAtRoute("GetRoleById",
                    new { roleId = roleViewModel.id },
                    roleViewModel);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Exception: ");
                return StatusCode(StatusCodes.Status500InternalServerError, ex);
            }
        }

        // DELETE: api/Role/5
        [HttpDelete("{roleId:int}")]
        //[Route("api/Roles/{roleId:int}")]
        public async Task<ActionResult> Delete(int roleId)
        {
            try
            {
                if (!_supervisor.RoleExists(roleId))
                {
                    return NotFound();
                }

                if (!await _supervisor.DeleteRoleAsync(roleId))
                {
                    return StatusCode(StatusCodes.Status500InternalServerError, "Error in remove Role");
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