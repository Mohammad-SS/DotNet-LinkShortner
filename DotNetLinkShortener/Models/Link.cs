using System;
using System.ComponentModel.DataAnnotations;

namespace DotNetLinkShortener.Models;

public class Link
{
    [Key] public int Id { get; set; }

    [Required] [MaxLength(2000)] public string TargetUrl { get; set; }

    [Required] [MaxLength(10)] public string Slug { get; set; }

    public DateTime Created { get; set; } = DateTime.UtcNow;
    public DateTime Modified { get; set; } = DateTime.UtcNow;
}