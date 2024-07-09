using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace UserManagementMVC.Controllers;
[Route("api/[controller]")]
[ApiController]
[Authorize(Roles = "Supervisor")]
public class SupervisorController : ControllerBase
{
    [HttpGet]
    [Route("GetData")]
    public IActionResult GetData()
    {
        var isSup = HttpContext.User.HasClaim(ClaimTypes.Role, "Supervisor");
        var auttype = HttpContext.User.Identity.AuthenticationType;
        var isauth = HttpContext.User.Identity.IsAuthenticated;
        var name = HttpContext.User.Identity.Name;
        return Ok("GetData() called by only supervisor user");
    }
}
