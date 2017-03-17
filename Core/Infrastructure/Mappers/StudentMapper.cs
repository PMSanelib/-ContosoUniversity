using Core.Commands.Students;
using Core.Models;
using Core.ViewModels;

namespace Core.Infrastructure.Mappers
{
    public static class StudentMapper
    {
        public static StudentModel Map(Student student)
        {
            var model = new StudentModel
            {
                Id = student.Id,
                FirstName = student.FirstName,
                LastName = student.LastName,
                EnrollmentDate = student.EnrollmentDate
            };

            if (student.Enrollments == null) return model;

            foreach (var enrollment in student.Enrollments)
            {
                model.Enrollments.Add(new StudentModel.Enrollment
                {
                    Id = enrollment.Id,
                    CourseId = enrollment.CourseId,
                    CourseName = enrollment.Course.Title,
                    Grade = enrollment.Grade.HasValue ? enrollment.Grade.Value.ToString() : null
                });
            }

            return model;
        }

        public static AddStudent Map(StudentModel model)
        {
            var command = new AddStudent
            {
                FirstName = model.FirstName,
                LastName = model.LastName,
                EnrollmentDate = model.EnrollmentDate
            };

            return command;
        }
    }
}
