namespace SGI.API.Controllers
{
    #region Using

    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.Logging;
    using SGI.Domain.Models;
    using SGI.Domain.Supervisor;
    using System;
    using System.Collections.Generic;
    using System.Linq;

    #endregion

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
        public ActionResult<IEnumerable<RoleViewModel>> GetRoles()
        {
            try
            {
                var result = _supervisor.GetAllRoles();
                return Ok(new { Items = result.ToList(), Count = result.Count() });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Exception: ");
                return StatusCode(500, ex.Message);
            }
        }

        // GET api/Roles/5
        [HttpGet("{roleId}", Name = "GetRoleById")]
        public ActionResult GetRoleById(int roleId)
        {
            try
            {
                if (!_supervisor.RoleExists(roleId))
                {
                    return NotFound();
                }

                return Ok(_supervisor.GetRoleById(roleId));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Exception: ");
                return StatusCode(500, ex.Message);
            }
        }

        [HttpPost]
        public ActionResult AddRole([FromBody]RoleViewModel roleViewModel)
        {
            try
            {
                var result = _supervisor.AddRole(roleViewModel);

                return CreatedAtRoute("GetRoleById",
                    new { roleId = result.id },
                    result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Exception: ");
                return StatusCode(500, ex.Message);
            }
        }

        [HttpPut]
        public ActionResult UpdateRole([FromBody]RoleViewModel roleViewModel)
        {
            try
            {
                if (roleViewModel == null)
                {
                    throw new ArgumentNullException(nameof(roleViewModel));
                }

                if (!_supervisor.RoleExists(roleViewModel.id))
                {
                    return NotFound();
                }

                _supervisor.UpdateRole(roleViewModel);

                return CreatedAtRoute("GetRoleById",
                    new { roleId = roleViewModel.id },
                    roleViewModel);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Exception: ");
                return StatusCode(500, ex);
            }
        }

        // DELETE: api/Role/5
        [HttpDelete("{roleId:int}")]
        //[Route("api/Roles/{roleId:int}")]
        public ActionResult Delete(int roleId)
        {
            try
            {
                if (!_supervisor.RoleExists(roleId))
                {
                    return NotFound();
                }

                _supervisor.DeleteRole(roleId);

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