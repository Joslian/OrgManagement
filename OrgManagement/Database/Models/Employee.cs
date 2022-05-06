using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using OrgManagement.Helpers;

namespace OrgManagement.Database.Models
{
    [Table("employee")]
    public class Employee : ViewModelBase
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

        private DateTime? _acceptedAt;
        [Column("accepted_at")]
        public DateTime? AcceptedAt
        {
            get => _acceptedAt;
            set
            {
                _acceptedAt = value;
                NotifyPropertyChanged(() => AcceptedAt);
            }
        }

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

            AcceptedAt = now;
            CreatedAt = now;
            UpdatedAt = now;
        }
    }
}
