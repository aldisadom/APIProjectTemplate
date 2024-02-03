namespace Domain.Entities;

public class BaseEntity
{
    public DateTime Created { get; set; }

    public string CreatedBy { get; set; } = string.Empty;

    public DateTime? Modified { get; set; }

    public string? ModifiedBy { get; set; }

    public bool IsDeleted { get; set; }
}