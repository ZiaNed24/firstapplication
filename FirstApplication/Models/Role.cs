using System;
using System.Collections.Generic;

namespace FirstApplication.Models
{
    public class Role
    {
        public int RoleId { get; set; }
        public string Name { get; set; }

        // Audit Fields
        public DateTime CreatedOn { get; set; } = DateTime.UtcNow;
        public int? CreatedBy { get; set; }
        public DateTime? ModifiedOn { get; set; }
        public int? ModifiedBy { get; set; }
        public bool IsActive { get; set; } = true;

        // Navigation
        public ICollection<User> Users { get; set; }
    }
}
