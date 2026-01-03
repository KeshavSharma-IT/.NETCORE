using System.ComponentModel.DataAnnotations;

namespace WebApiCodeFirst.Models
{
    public class Employee
    {
        [Key]
        public int EmpId { get; set; }
        [Required(ErrorMessage ="Employee Name is required")]
        [StringLength(200,MinimumLength =3,ErrorMessage ="Employee Name must be between 3 to 200 ")]
        public string EmpName { get; set; }
        public int EmpAge { get; set; }
        public string EmpGender { get; set; }
        public DateTime EmpDOJ { get; set; }
        public string Designation { get; set; }
        public int Salary { get; set; }
    }
}
