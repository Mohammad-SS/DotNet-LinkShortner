namespace DotNetLinkShortener.DTOs;

public class LinkViewDto
{
    public int LinkId { get; set; }

    public string UserAgent { get; set; }

    public string IpAddress { get; set; }

    public DateTime ViewedAt { get; set; }
}