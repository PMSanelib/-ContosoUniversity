using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Core.ViewModels
{
    public class StudentModel
    {
        public int Id { get; set; }

        [Required]
        [StringLength(50)]
        [Display(Name = "Last Name")]
        public string LastName { get; set; }


        [Required]
        [StringLength(50, ErrorMessage = "First name cannot be longer than 50 characters.")]
        [Display(Name = "First Name")]
        public string FirstName { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [Display(Name = "Enrollment Date")]
        public DateTime EnrollmentDate { get; set; }

        [Display(Name = "Full Name")]
        public string FullName => LastName + ", " + FirstName;

        public ICollection<Enrollment> Enrollments { get; set; } = new List<Enrollment>();

        public class Enrollment
        {
            public int Id { get; set; }
            public int CourseId { get; set; }
            public virtual string CourseName { get; set; }
            public string Grade { get; set; }
        }
    }
}
