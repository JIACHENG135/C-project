using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.Caching;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WebApplication1.Models;
using System.Web;

namespace WebApplication1.Controllers
{
    public class HomeController : Controller
    {
        ObjectCache cache = MemoryCache.Default;
        List<Students> stus; 
        public HomeController()
        {
            stus = cache["stus"] as List<Students>;
            if (stus == null)
            {
                stus = new List<Students>();
            }
        }
        public void SaveCache()
        {
            cache["stus"] = stus;
        }
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";

            return View();
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
        }
        public ActionResult ViewStudent(string id)
        {
            Console.WriteLine(id);
            Students stu = stus.FirstOrDefault(s => s.Id == id);
            if (stu == null)
            {
                return View();
            }
            else
            {
                return View(stu);
            }
        }



        public ActionResult AddStudent()
        {
            return View();

        }
        [HttpPost]
        public ActionResult AddStudent(Students stu)
        {
            if (!ModelState.IsValid)
            {
                return View(stu);
            }
            stu.Id = Guid.NewGuid().ToString();
            stus.Add(stu);
            SaveCache();
            return RedirectToAction("StudentsList");
        }
        public ActionResult EditStudent(string id)
        {
            Students stu = stus.FirstOrDefault(s => s.Id == id);
            if (stu == null)
            {
                return View();
            }
            else
            {
                return View(stu);
            }
        }
        [HttpPost]
        public ActionResult EditStudent(Students stu, string id)
        {
            var stuToEdit = stus.FirstOrDefault(s => s.Id == id);
            if (stu == null)
            {
                return View();
            }
            else
            {
                stuToEdit.Name = stu.Name;
                stuToEdit.Telephone = stu.Telephone;
                SaveCache();
                return RedirectToAction("StudentsList");
            }
        }
        public ActionResult DeleteStudent(string id)
        {
            Students stu = stus.FirstOrDefault(s => s.Id == id);
            if (stu == null)
            {
                return View();
            }else{
                return View(stu);
            }
        }
        [HttpPost]
        [ActionName("DeleteStudent")]
        public ActionResult ConfirmDeleteStudent(string id)
        {
            Students stu = stus.FirstOrDefault(s => s.Id == id);
            if (stu == null)
            {
                return View();
            }
            else
            {
                stus.Remove(stu);
                return RedirectToAction("StudentsList");
            }
        }
        public ActionResult StudentsList()
        {
            return View(stus);
        }
        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
