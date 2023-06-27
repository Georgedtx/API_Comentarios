using Microsoft.EntityFrameworkCore;

namespace ComentariosApi.Models;

public class ComentarioContext : DbContext
{
    public ComentarioContext(DbContextOptions<ComentarioContext> options)
        : base(options)
    {
    }

    public DbSet<ComentarioItem> ComentarioItems { get; set; } = null!;
}