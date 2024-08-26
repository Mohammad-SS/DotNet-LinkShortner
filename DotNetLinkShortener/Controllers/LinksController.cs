using Microsoft.AspNetCore.Mvc;
using DotNetLinkShortener.Data;
using DotNetLinkShortener.DTOs;
using DotNetLinkShortener.Models;

namespace DotNetLinkShortener.Controllers;

[ApiController]
[Route("api/[controller]")]
public class LinksController : ControllerBase
{
    private readonly LinkShortnerDbContext _context;

    public LinksController(LinkShortnerDbContext context)
    {
        _context = context;
    }

    [HttpGet]
    public ActionResult<IEnumerable<LinkDto>> GetLinks()
    {
        var links = _context.Links.Select(link => new LinkDto
        {
            TargetUrl = link.TargetUrl,
            Slug = link.Slug,
        }).ToList();

        return Ok(links);
    }


    [HttpPost]
    public ActionResult<LinkDto> CreateLink([FromBody] LinkDto linkDto)
    {
        var link = new Link
        {
            TargetUrl = linkDto.TargetUrl,
            Slug = linkDto.Slug,
        };

        _context.Links.Add(link);
        _context.SaveChanges();

        return CreatedAtAction(nameof(GetLinks), new { id = link.Id }, linkDto);
    }

    [HttpGet("/{slug}")]
    public IActionResult RedirectToTarget(string slug)
    {
        var link = _context.Links.FirstOrDefault(link => link.Slug == slug);
        if (link == null)
        {
            return NotFound();
        }

        string ipAddress = Request.Headers["X-Forwarded-For"].FirstOrDefault();

        if (string.IsNullOrEmpty(ipAddress))
        {
            ipAddress = HttpContext.Connection.RemoteIpAddress?.ToString();
        }

        string userAgent = Request.Headers["User-Agent"].ToString();

        var linkView = new LinkView
        {
            LinkId = link.Id,
            UserAgent = userAgent,
            IpAddress = ipAddress,
            ViewedAt = DateTime.UtcNow
        };
        _context.LinkViews.Add(linkView);
        _context.SaveChanges();

        return Redirect(link.TargetUrl);
    }

    [HttpGet("/{slug}/stats")]
    public ActionResult GetLinkStats(string slug)
    {
        var link = _context.Links.FirstOrDefault(l => l.Slug == slug);
        if (link == null)
        {
            return NotFound();
        }

        // Total views
        var totalViews = _context.LinkViews.Count(lv => lv.LinkId == link.Id);

        // Unique views based on IP address
        var uniqueViews = _context.LinkViews
            .Where(lv => lv.LinkId == link.Id)
            .Select(lv => lv.IpAddress)
            .Distinct()
            .Count();

        return Ok(new
        {
            TotalViews = totalViews,
            UniqueViews = uniqueViews
        });
    }
}