using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using UserManagementMVC.DTO;
using UserMgmtDAL.Entities;
using UserMgmtDAL.Repository;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace UserManagementMVC.Controllers;
[Route("api/[controller]")]
[ApiController]
[Authorize(Roles ="Admin")]
public class ManageUsersAPIController : ControllerBase
{
    private readonly IRepository<User> repository;

    public ManageUsersAPIController(IRepository<User> repository)
    {
        this.repository = repository;
    }

    [HttpGet]
    [Route("GetUsers")]
    [ActionName("GetUsers")]
    public IActionResult Get()
    {
        var users = repository.GetAll();
        if (users == null)
            return NotFound("Users are not aviable in DB");
        {
            var usersdto = users.Select(usr => new UserDto
            {
                 Id=usr.Id,
                  Name=usr.Name,
                   DOB=usr.DOB,
                    Email=usr.Email,
                     Mobile=usr.Mobile,
                      RoleName=usr.Role.Name
            });
            return Ok(usersdto);
        }
    }

    // GET api/<ManageUsersAPIController>/5
    [HttpGet()]
    [Route("GetUserById/{id}")]
    [ActionName("GetUserById")]
    public IActionResult GetById(int id)
    {
        var usr = repository.GetById(id);
        if (usr == null)
            return NotFound("Users for id not found");
        {
            var userdto = new UserDto
            {
                Id = usr.Id,
                Name = usr.Name,
                DOB = usr.DOB,
                Email = usr.Email,
                Mobile = usr.Mobile,
                RoleName = usr.Role.Name
            };
            return Ok(userdto);
        }
    }

   
    [HttpPost]
    [Route("AddUser")]
    public IActionResult Post([FromBody] UserDto user)
    {
        try
        {
            var u = new User
            {
                 Name=user.Name,
                  DOB=user.DOB,
                   Email=user.Email,
                    Mobile=user.Mobile,
                     RoleId=user.RoleId
            };
            int id = repository.Add(u);
            if (id > 0)
            {
                return CreatedAtAction("GetUserById", new { Id = id });
            }
            else
                return BadRequest();
        }
        catch (Exception ex)
        {
            return StatusCode(StatusCodes.Status500InternalServerError
                ,new {error=ex.Message});
        }
    }

    // PUT api/<ManageUsersAPIController>/5
    [HttpPut("{id}")]
    public void Put(int id, [FromBody] string value)
    {
    }

    // DELETE api/<ManageUsersAPIController>/5
    [HttpDelete("{id}")]
    public void Delete(int id)
    {
    }
}
