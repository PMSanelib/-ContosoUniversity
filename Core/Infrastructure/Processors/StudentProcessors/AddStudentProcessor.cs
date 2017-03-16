using Core.Commands.Students;
using Core.Dtos;
using Core.Models;

namespace Core.Infrastructure.Processors.StudentProcessors
{
    public class AddStudentProcessor : ICommandProcessor<AddStudent>
    {
        private readonly ApplicationDbContext _context;

        public AddStudentProcessor(ApplicationDbContext context)
        {
            _context = context;
        }

        public ValidationResult Process(AddStudent command)
        {
            var entity = new Student
            {
                FirstName = command.FirstName,
                LastName = command.LastName,
                EnrollmentDate = command.EnrollmentDate
            };

            _context.Students.Add(entity);

            _context.SaveChanges();

            return new ValidationResult();
        }
    }
}
