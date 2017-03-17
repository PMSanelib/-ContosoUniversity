using Core.Commands.Students;
using Core.Dtos;

namespace Core.Infrastructure.Processors.StudentProcessors
{
    public class DeleteStudentProcessor : ICommandProcessor<DeleteStudent>
    {
        private readonly ApplicationDbContext _context;

        public DeleteStudentProcessor(ApplicationDbContext context)
        {
            _context = context;
        }

        public ValidationResult Process(DeleteStudent command)
        {
            var result = new ValidationResult();

            var student = _context.Students.Find(command.Id);

            if (student == null)
            {
                result.AddError("Id", "student not found");
                return result;
            }

            _context.Students.Remove(student);

            return result;
        }
    }
}