using Domain;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using WebAPI.Identity.Dto;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WebAPI.Identity.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RolesController : ControllerBase
    {
        private readonly RoleManager<Role> _roleManager;
        private readonly UserManager<User> _userManager;

        public RolesController(RoleManager<Role> roleManager, UserManager<User> userManager)
        {
            _roleManager = roleManager;
            _userManager = userManager;
        }

        // GET: api/<RolesController>
        [HttpGet]
        [Authorize(Roles = "Admin")]
        public IActionResult Get()
        {
            return Ok(new
            {
                role = new RoleDto(),
                UpdateUserRole = new UpdateUserRoleDto()
            });
        }

        // GET api/<RolesController>/5
        [HttpGet("{id}", Name = "Get")]
        [Authorize(Roles = "Admin, Gerente")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<RolesController>
        [HttpPost("CreateRole")]
        public async Task<IActionResult> CreateRole(RoleDto dto)
        {
            try
            {
                var result = await _roleManager.CreateAsync(new Role { Name = dto.Name });

                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    $"Error: {ex.Message}");
            }
        }

        // PUT api/<RolesController>/5
        [HttpPut("UpdateUserRoles")]
        public async Task<IActionResult> UpdateUserRoles(UpdateUserRoleDto dto)
        {
            try
            {
                var user = await _userManager.FindByEmailAsync(dto.Email);

                if (user != null)
                {
                    if (dto.Delete)
                        await _userManager.RemoveFromRoleAsync(user, dto.Role);
                    else
                        await _userManager.AddToRoleAsync(user, dto.Role);
                }
                else
                {
                    return NotFound("Usuário não encontrado.");
                }

                return Ok("Success");
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    $"Error: {ex.Message}");
            }
        }

        // DELETE api/<RolesController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
