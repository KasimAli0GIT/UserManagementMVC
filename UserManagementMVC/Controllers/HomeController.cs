using Microsoft.AspNetCore.Mvc;
using UserManagementMVC.Models;

namespace UserManagementMVC.Controllers;

[Route("[controller]")]
public class HomeController : Controller
{
    [Route("MyIndex")]
    public string Index()
    {

        return "Index called";
    }
    [Route("MyCreate")]
    public string Create()
    {

        return "Create called";
    }
    [Route("MyEdit")]
    public string Edit()
    {
        var id = this.HttpContext.Request.Query["id"];
        return "Edit called " + id ;
    }
    [Route("MyDelete/{id?}")]
    public string Delete(int id=0)
    {

        return "Delete called";
    }
    [Route("MyFind/{name}/{loc?}")]
    public string Find(string name="",string loc="")
    {

        return "Find called";
    }
    [Route("Greetings/{name}")]
    public ViewResult Greetings(string name="")
    {
        //logic

        //dynamic mydny = new Employee();
        //mydny.Join();

        //ViewBag.Name = name;
        //ViewBag.DOB = new DateTime(1999, 1, 1);
        //ViewBag.Salary = 3333.3f;

        Employee employee = new Employee
        {
            Name = name,
            DOB=new DateTime(1997,1,3),
            Salary=343434.3f
        };

        //   ViewBag.Employee = employee;

        return View(employee);
    }

    [Route("Divide")]
    public IActionResult DoDivide()
    {
        int i = 10;
        int j = 0;
        int result = i / j;
        ViewBag.Result = result;
        return View();
    }
    [Route("Error")]
    public IActionResult Error()
    {
        return View();
    }



}
