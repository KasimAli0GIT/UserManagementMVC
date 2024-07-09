using Microsoft.AspNetCore.Mvc;
using UserManagementMVC.ViewModels;
using UserMgmtDAL.Entities;
using UserMgmtDAL.Repository;

namespace UserManagementMVC.Controllers;
public class ManageUserController : Controller
{
    private readonly IRepository<User> repository;

    public ManageUserController(IRepository<User> repository)
    {
        this.repository = repository;
    }
    [HttpGet]
    public IActionResult Index()
    {
        var users = repository.GetAll();
        return View(users);
    }
    [HttpGet]
    public IActionResult Create()
    {
        var usrvm = new UserEntryVM();
        return View(usrvm);
    }
    [HttpPost]
    public IActionResult Create(UserEntryVM vm)
    {
        if(ModelState.IsValid)
        {
            //add to db
            User u = new User
            {
                Name = vm.Name,
                DOB = vm.DOB, Email = vm.Email, Mobile = vm.Mobile, RoleId = vm.RoleId
            };
            int newid = repository.Add(u);
            if (newid > 0)
                return RedirectToAction("Index");
            else
                ModelState.AddModelError("", "Insert was not successfull");
            return View(vm);
        }else
        {
            return View(vm);
        }
        //var usrvm = new UserEntryVM();
    }
    [HttpGet]
    public IActionResult Edit(int id = 0)
    {
        return View();
    }
}
