using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OrgManagement.Database.Models
{
    [Table("admins")]
    public class Admin
    {
        [Column("auto_id"), Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public uint AutoId { get; set; }

        [Column("login"), Required]
        public string Login { get; set; }

        [Column("password"), Required]
        public string Password { get; set; }

        [Column("created_at")]
        public DateTime? CreatedAt { get; set; }

        [Column("updated_at")]
        public DateTime? UpdatedAt { get; set; }

        public Admin(string login, string password)
        {
            var now = DateTime.Now;

            Login = login;
            Password = password;

            CreatedAt = now;
            UpdatedAt = now;
        }
    }
}
