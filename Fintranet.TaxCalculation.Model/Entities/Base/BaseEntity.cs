using System;
using System.ComponentModel.DataAnnotations;

public class BaseEntity
{
    [Key]
    public Guid Id { get; private set; }
    public DateTime CreatedAt { get; private set; }
    public DateTime UpdatedAt { get; private set; }
    public bool IsDeleted { get; private set; }
    public BaseEntity()
    {
        Id = Guid.NewGuid();
        CreatedAt = DateTime.Now;
        UpdatedAt = DateTime.Now;
        IsDeleted = false;

    }
    public void Delete()
    {
        IsDeleted = true;
        UpdatedAt = DateTime.Now;
    }


}