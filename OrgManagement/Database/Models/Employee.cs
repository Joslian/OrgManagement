using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OrgManagement.Database.Models
{
    [Table("employees")]
    public class Employee
    {
        [Column("auto_id"), Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public uint AutoId { get; set; }

        [Column("first_name"), Required]
        public string FirstName { get; set; }

        [Column("last_name"), Required]
        public string LastName { get; set; }

        [Column("middle_name"), Required]
        public string MiddleName { get; set; }

        [Column("created_at")]
        public DateTime? CreatedAt { get; set; }

        [Column("updated_at")]
        public DateTime? UpdatedAt { get; set; }

        public Employee(string firstName, string lastName, string middleName)
        {
            var now = DateTime.Now;

            FirstName = firstName;
            LastName = lastName;
            MiddleName = middleName;

            CreatedAt = now;
            UpdatedAt = now;
        }
    }
}
