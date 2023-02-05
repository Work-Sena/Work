using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using Work.Models.Entity;

namespace Work.Controllers
{
    [AllowAnonymous]
    [Authorize(Roles = "A")]
   
    public class EmployeeController : Controller
    {
        // GET: Employee
        
        WorkDbEntities db = new WorkDbEntities();
       
        public ActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Login(Tbl_Employee p)
        {
            var employee=db.Tbl_Employee.FirstOrDefault(x=>x.Email== p.Email&&x.Password==p.Password);//BUNUN YERİNE Equals kullan
            if (employee!=null) 
            {
                FormsAuthentication.SetAuthCookie(employee.Email,false);
                return RedirectToAction("Index", "HomePage");
            }
            else
            {
                ViewBag.Message = "Kullanıcı adı veya şifre hatalı";
            }                       
            return View();
        }
        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Login");
        }
    }
}