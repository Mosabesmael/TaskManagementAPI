using System;
using System.Collections.Generic;
using System.Text;
namespace TaskManagementAPI.Models;

public class TaskItem
{
    public int Id { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
    public bool IsCompleted { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime? DueDate { get; set; }

    public override string ToString()
    {
        return $"[{Id}] {Title} - {(IsCompleted ? "✓" : "○")}";
    }

    /// احسب كام يوم متبقي للـ deadline
    public int ? DaysUntilDue()
    {
        if (DaysUntilDue == null) return null;

        return (int)(DueDate.Value.Date - DateTime.Now.Date).TotalDays;
    }

    /// شُوف لو المهمة متأخرة
    public bool IsOverdue()
    {
        return DueDate < DateTime.Now && !IsCompleted;
    }
}
