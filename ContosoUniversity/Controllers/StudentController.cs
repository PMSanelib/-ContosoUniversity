using System.Data.Entity.Infrastructure;
using System.Web.Mvc;
using System.Net;
using Core.Commands.Students;
using Core.Infrastructure.Mappers;
using Core.Infrastructure.ModelServices;
using Core.Models;
using Core.ViewModels;

namespace ContosoUniversity.Controllers
{
    public class StudentController : SmartController
    {
        // GET: Student
        public ViewResult Index(string sortOrder, string currentFilter, string searchString, int? page)
        {
            ViewBag.CurrentSort = sortOrder;
            ViewBag.NameSortParm = string.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
            ViewBag.DateSortParm = sortOrder == "Date" ? "date_desc" : "Date";

            if (searchString != null)
            {
                page = 1;
            }
            else
            {
                searchString = currentFilter;
            }

            ViewBag.CurrentFilter = searchString;

            return View(GetService<IStudentModelService>().SearchWithPaging(sortOrder, searchString, page ?? 1));
        }


        // GET: Student/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var student = GetService<IStudentModelService>().GetById(id.Value, true);

            if (student == null)
            {
                return HttpNotFound();
            }

            return View(student);
        }

        // GET: Student/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Student/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "LastName, FirstName, EnrollmentDate")] StudentModel student)
        {
            if (!ModelState.IsValid) return View(student);

            var result = ExecuteCommand(StudentMapper.MapAddStudent(student));

            if (result.IsValid) return RedirectToAction("Index");

            ModelState.AddModelError("", "Unable to save changes. Try again, and if the problem persists, see your system administrator.");

            return View(student);
        }

        // GET: Student/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var student = GetService<IStudentModelService>().GetById(id.Value, true);

            if (student == null)
            {
                return HttpNotFound();
            }

            return View(student);
        }

        // POST: Student/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost, ActionName("Edit")]
        [ValidateAntiForgeryToken]
        public ActionResult EditPost(StudentModel model)
        {
            if (!ModelState.IsValid) return View(model);

            var result = ExecuteCommand(StudentMapper.MapUpdateStudent(model));

            if (result.IsValid) return RedirectToAction("Index");

            ModelState.AddModelError("", "Unable to save changes. Try again, and if the problem persists, see your system administrator.");

            return View(model);
        }

        // GET: Student/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var student = GetService<IStudentModelService>().GetById(id.Value, true);

            if (student == null)
            {
                return HttpNotFound();
            }

            return View(student);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            var result = ExecuteCommand(new DeleteStudent { Id = id });

            if (result.IsValid) return RedirectToAction("Index");

            ModelState.AddModelError("", "Unable to save changes. Try again, and if the problem persists, see your system administrator.");

            return View(GetService<IStudentModelService>().GetById(id, true));
        }
    }
}
