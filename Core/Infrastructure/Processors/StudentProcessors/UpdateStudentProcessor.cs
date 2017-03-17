using System.Data.Entity.Migrations;
using Core.Commands.Students;
using Core.Dtos;

namespace Core.Infrastructure.Processors.StudentProcessors
{
    public class UpdateStudentProcessor : ICommandProcessor<UpdateStudent>
    {
        private readonly ApplicationDbContext _context;

        public UpdateStudentProcessor(ApplicationDbContext context)
        {
            _context = context;
        }

        public ValidationResult Process(UpdateStudent command)
        {
            var result = new ValidationResult();

            var studentToUpdate = _context.Students.Find(command.Id);

            if (studentToUpdate == null)
            {
                result.AddError("Id", "student not found");
                return result;
            }

            studentToUpdate.EnrollmentDate = command.EnrollmentDate;
            studentToUpdate.LastName = command.LastName;
            studentToUpdate.FirstName = command.FirstName;

            _context.Students.AddOrUpdate(studentToUpdate);

            return result;
        }
    }
}