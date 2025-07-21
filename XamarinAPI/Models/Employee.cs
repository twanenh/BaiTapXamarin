using System.ComponentModel.DataAnnotations;

namespace XamarinAPI.Models
{
    public class Employee
    {
        public int Id { get; set; }
        [MaxLength(500)]
        public string Name { get; set; }
        [MaxLength(500)]
        public string Role { get; set; }

        public int DepartmentId { get; set; }
        public Department Department { get; set; }
    }

}
