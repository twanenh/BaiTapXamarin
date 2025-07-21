using System.ComponentModel.DataAnnotations;

namespace XamarinAPI.Models
{
    public class Department
    {
        public int Id { get; set; }
        [MaxLength(500)]
        public string Name { get; set; }

        public ICollection<Employee> Employees { get; set; }
    }
}
