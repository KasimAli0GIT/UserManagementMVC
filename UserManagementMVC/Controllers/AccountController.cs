using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using UserManagementMVC.Models;
using UserManagementMVC.Services;
using UserMgmtDAL.Entities;
using UserMgmtDAL.Repository;

namespace UserManagementMVC.Controllers;
[Route("api/[controller]")]
[ApiController]
[AllowAnonymous]
public class AccountController : ControllerBase
{
    private readonly IAuth auth;
    private readonly ITokenService tokenService;

    public AccountController(IAuth auth,ITokenService tokenService)
    {
        this.auth = auth;
        this.tokenService = tokenService;
    }

    [HttpPost]
    [Route("Login")]
    public IActionResult Login(LoginData loginData)
    {
        string token = "";
        //auth user
        User usr = auth.DoLogin(loginData.UserName, loginData.Password);
        if (usr == null)
            return BadRequest("Username/password invalid");
        else
        {
            //generate the token
             token= tokenService.GetJwtToken(usr.Email, usr.Role.Name);

            //return the token 
        }
        return Ok(new { mytoken=token });
    }
}
