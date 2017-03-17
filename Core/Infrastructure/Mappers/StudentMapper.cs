using Core.Commands.Students;
using Core.Models;
using Core.ViewModels;

namespace Core.Infrastructure.Mappers
{
    public static class StudentMapper
    {
        public static StudentModel MapStudentModel(Student student)
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

        public static AddStudent MapAddStudent(StudentModel model)
        {
            var command = new AddStudent
            {
                FirstName = model.FirstName,
                LastName = model.LastName,
                EnrollmentDate = model.EnrollmentDate
            };

            return command;
        }

        public static UpdateStudent MapUpdateStudent(StudentModel model)
        {
            var command = new UpdateStudent
            {
                Id = model.Id,
                FirstName = model.FirstName,
                LastName = model.LastName,
                EnrollmentDate = model.EnrollmentDate
            };

            return command;
        }
    }
}
