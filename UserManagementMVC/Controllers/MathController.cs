using Microsoft.AspNetCore.Mvc;

namespace UserManagementMVC.Controllers;
public class MathController : Controller
{
    public IActionResult Index()
    {
        return View();
    }
    public IActionResult DoMath()
    {
        var txt1 = Request.Form["txtNum1"];
        var txt2 = Request.Form["txtNum2"];
        var opr = Request.Form["sltOpr"];

        var n1 = Convert.ToInt32(txt1);
        var n2 = Convert.ToInt32(txt2);

        long result = 0;
        if(opr == "add")
        {
            result = n1 + n2;
        }else
        {
            if(n1 > n2)
            {
                result = n1 - n2;

            }else
            {
                //error
                // ShowError()
                //  return RedirectToAction("ShowError");
                //ViewBag.IsError = true;
                TempData["IsError"] = true;
                return RedirectToAction("Index");
            }
        }
        ViewBag.Result = result;
        return View();
    }

    public IActionResult ShowError()
    {
        return View();
    }
}
