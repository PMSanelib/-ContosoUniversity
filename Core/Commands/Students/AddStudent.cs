using System;

namespace Core.Commands.Students
{
    public class AddStudent : ICommand
    {
        public string LastName { get; set; }
        public string FirstName { get; set; }
        public DateTime EnrollmentDate { get; set; }
    }
}
