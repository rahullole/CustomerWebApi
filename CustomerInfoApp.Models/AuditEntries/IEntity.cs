﻿namespace CustomerInfoApp.Models.AuditEntries
{
    public interface IEntity<T>
    {
        T Id { get; set; }
    }
}
