using System;

namespace CustomerInfoApp.Models.AuditEntries
{
    public abstract class AuditableEntity<T> : Entity<T>, IAuditableEntity
    {
        public bool Status { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime UpdatedDate { get; set; }
    }
}
