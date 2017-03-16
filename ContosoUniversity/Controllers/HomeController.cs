using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Core.Infrastructure;
using Core.ViewModels;


namespace ContosoUniversity.Controllers
{
    public class HomeController : Controller
    {
        private readonly ApplicationDbContext _context;

        public HomeController(ApplicationDbContext context)
        {
            _context = context;
        }

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            // Commenting out LINQ to show how to do the same thing in SQL.
            //IQueryable<EnrollmentDateGroup> = from student in db.Students
            //           group student by student.EnrollmentDate into dateGroup
            //           select new EnrollmentDateGroup()
            //           {
            //               EnrollmentDate = dateGroup.Key,
            //               StudentCount = dateGroup.Count()
            //           };

            // SQL version of the above LINQ code.
            string query = "SELECT EnrollmentDate, COUNT(*) AS StudentCount "
                + "FROM Person "
                + "WHERE Discriminator = 'Student' "
                + "GROUP BY EnrollmentDate";

            IEnumerable<EnrollmentDateGroup> data = _context.Database.SqlQuery<EnrollmentDateGroup>(query);

            return View(data.ToList());
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Contact Information" + _context.GetHashCode();

            return View();
        }
    }
}