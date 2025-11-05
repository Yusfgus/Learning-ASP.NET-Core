using System;

namespace EF_Core.QueryData.Entities;

public class Review : Entity
{
    public string? Feedback { get; set; }

    public int CourseId { get; set; }

    public Course Course { get; set; } = null!;

    public DateTime CreatedAt { get; set; }
}
