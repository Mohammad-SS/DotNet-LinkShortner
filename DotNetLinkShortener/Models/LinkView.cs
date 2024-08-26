
namespace DotNetLinkShortener.Models;

public class LinkView
{
    public int Id { get; set; }

    public int LinkId { get; set; } 

    public string UserAgent { get; set; }
    
    public string IpAddress { get; set; } 

    public DateTime ViewedAt { get; set; } = DateTime.UtcNow;

    public Link Link { get; set; }

}