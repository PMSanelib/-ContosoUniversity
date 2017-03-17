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
