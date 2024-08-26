using DotNetLinkShortener.Models;
using Microsoft.EntityFrameworkCore;

namespace DotNetLinkShortener.Data;

public class LinkShortnerDbContext : DbContext
{
    public LinkShortnerDbContext(DbContextOptions<LinkShortnerDbContext> options) : base(options)
    {
    }

    public DbSet<Link> Links { get; set; }
}