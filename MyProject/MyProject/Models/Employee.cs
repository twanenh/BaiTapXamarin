﻿namespace XamarinAPI.Models
{
    public class Employee
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Role { get; set; }

        public int DepartmentId { get; set; }
        public Department Department { get; set; }
    }

}
