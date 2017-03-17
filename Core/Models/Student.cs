using System;
using System.Collections.Generic;

namespace Core.Models
{
    public class Student : Person
    {
        public DateTime EnrollmentDate { get; set; }
        public ICollection<Enrollment> Enrollments { get; set; }
    }
}