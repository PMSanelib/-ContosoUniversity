using System.Collections.Generic;
using System.Linq;
using Core.Infrastructure.Mappers;
using Core.ViewModels;
using PagedList;

namespace Core.Infrastructure.ModelServices
{
    public interface IStudentModelService
    {
        IPagedList<StudentModel> SearchWithPaging(string sortOrder, string searchString, int page);
        StudentModel GetById(int id, bool includeEnrollments = false);
    }

    public class StudentModelService : IStudentModelService
    {
        private readonly ApplicationDbContext _context;

        public StudentModelService(ApplicationDbContext context)
        {
            _context = context;
        }

        public IPagedList<StudentModel> SearchWithPaging(string sortOrder, string searchString, int page)
        {
            var students = from s in _context.Students select s;

            if (!string.IsNullOrEmpty(searchString))
            {
                students = students.Where(s => s.LastName.Contains(searchString) || s.FirstName.Contains(searchString));
            }

            switch (sortOrder)
            {
                case "name_desc":
                    students = students.OrderByDescending(s => s.LastName);
                    break;
                case "Date":
                    students = students.OrderBy(s => s.EnrollmentDate);
                    break;
                case "date_desc":
                    students = students.OrderByDescending(s => s.EnrollmentDate);
                    break;
                default:  // Name ascending 
                    students = students.OrderBy(s => s.LastName);
                    break;
            }

            var list = new List<StudentModel>();

            foreach (var student in students)
            {
                list.Add(StudentMapper.Map(student));
            }

            const int pageSize = 3;
            var pageNumber = page;

            return list.ToPagedList(pageNumber, pageSize);
        }

        public StudentModel GetById(int id, bool includeEnrollments = false)
        {
            var student = _context.Students.Find(id);
            return student == null ? null : StudentMapper.Map(student);
        }
    }
}
