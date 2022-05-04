using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OrgManagement.Database.Models
{
    [Table("employee")]
    public class Employee
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public uint EmployeeId { get; set; }

        [Column("first_name"), Required]
        public string FirstName { get; set; }

        [Column("last_name"), Required]
        public string LastName { get; set; }

        [Column("middle_name"), Required]
        public string MiddleName { get; set; }

        [Column("salary"), Required]
        public uint Salary { get; set; }

        [Column("department_id"), ForeignKey("department")]
        public uint? DepartmentId { get; set; }

        [Column("post_id"), ForeignKey("post")]
        public uint? PostId { get; set; }

        [Column("created_at")]
        public DateTime? CreatedAt { get; set; }

        [Column("updated_at")]
        public DateTime? UpdatedAt { get; set; }

        public Employee(string lastName, string firstName, string middleName, uint salary)
        {
            var now = DateTime.Now;

            LastName = lastName;
            FirstName = firstName;
            MiddleName = middleName;

            Salary = salary;

            CreatedAt = now;
            UpdatedAt = now;
        }
    }
}
