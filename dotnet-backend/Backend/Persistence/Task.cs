using System;
using System.Collections.Generic;

namespace Backend.Persistence;

public partial class Task
{
    public int Id { get; set; }

    public string? Name { get; set; }

    public string? Description { get; set; }

    public DateTime? StartDate { get; set; }

    public DateTime? DueDate { get; set; }

    public int? FkTypeId { get; set; }

    public int? FkPriorityId { get; set; }

    public virtual Priority? FkPriority { get; set; }

    public virtual Type? FkType { get; set; }
}
