using Microsoft.AspNetCore.Mvc;
using UserManagementMVC.Models;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace UserManagementMVC.Controllers;
[Route("api/[controller]")]
[ApiController]
public class FirstAPIController : ControllerBase
{
    // GET: api/<FirstAPIController>
    [HttpGet]
    public IEnumerable<string> Get()
    {
        return new string[] { "value1", "value2" };
    }

    // GET api/<FirstAPIController>/5
    [HttpGet("{id}")]
    public string Get(int id)
    {
        return "value";
    }

    // POST api/<FirstAPIController>
    [HttpPost]
    public void Post([FromBody] string value)
    {
    }

    // PUT api/<FirstAPIController>/5
    [HttpPut("{id}")]
    public void Put(int id, [FromBody] string value)
    {
    }

    // DELETE api/<FirstAPIController>/5
    [HttpDelete("{id}")]
    public void Delete(int id)
    {
    }

    [HttpPost]
    [Route("DoLogin")]
    public IActionResult DoLogin(LoginData login)
    {
        if (login.UserName == "" || login.Password == "")
            return BadRequest(new { Message = "Username or password empty" });

        if (login.UserName == "slk" && login.Password == "api")
        {
            return Ok(new { Message = "Successfully logged in" });
        }
        else
            return NotFound(new { Message = "Username/password not found" });
    }
}
