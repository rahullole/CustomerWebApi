using System;

namespace CustomerInfoApp.Models.AuditEntries
{
    public interface IAuditableEntity
    {
        bool Status { get; set; }
        DateTime CreatedDate { get; set; }
        DateTime UpdatedDate { get; set; }
    }
}
