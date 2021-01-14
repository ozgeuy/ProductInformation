using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using ProductInformation.Models;
using System.Text;
using System.Threading.Tasks;

namespace ProductInformation.Controllers
{
    public class StudentController : Controller
    {
        SchoolContext objContext;
        public StudentController()
        {
            objContext = new SchoolContext();
        }
        #region List and Details Students   
        public ActionResult Index()
        {
            var studs = objContext.Students.ToList();
            return View(studs);
        }
        public ViewResult Details(int id)
        {
            Student stud = objContext.Students.Where(x => x.StudentId == id).SingleOrDefault();
            return View(stud);
        }
        #endregion
        #region Create Student   
        public ActionResult Create()
        {
            return View(new Student());
        }
        [HttpPost]
        public ActionResult Create(Student stud)
        {
            objContext.Students.Add(stud);
            objContext.SaveChanges();
            return RedirectToAction("Index");
        }
        #endregion
        #region Edit Student
        public ActionResult Edit(int id)
        {
            Student stud = objContext.Students.Where(x => x.StudentId == id).SingleOrDefault();
            return View(stud);
        }
        [HttpPost]
        public ActionResult Edit(Student model)
        {
            Student stud = objContext.Students.Where(x => x.StudentId == model.StudentId).SingleOrDefault();
            if (stud != null)
            {
                objContext.Entry(stud).CurrentValues.SetValues(model);
                objContext.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(stud);
        }
        #endregion
        #region Delete Student   
        public ActionResult Delete(int id)
        {
            Student stud = objContext.Students.Find(id);
            return View(stud);
        }
        [HttpPost]
        public ActionResult Delete(int id, Student model)
        {
            var stud = objContext.Students.Where(x => x.StudentId == id).SingleOrDefault();
            if (stud != null)
            {
                objContext.Students.Remove(stud);
                objContext.SaveChanges();
            }
            return RedirectToAction("Index");
        }
        #endregion
    }
}
