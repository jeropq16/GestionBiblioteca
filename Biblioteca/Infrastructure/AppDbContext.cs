using BiblioAwoo.Models;
using Microsoft.EntityFrameworkCore;

namespace BiblioAwoo.Infrastructure;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

    public DbSet<Usuario> Usuarios { get; set; }
    public DbSet<Libro> Libros { get; set; }
    public DbSet<Prestamo> Prestamos { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Usuario>().HasIndex(u=>u.Documento).IsUnique();
        modelBuilder.Entity<Libro>().HasIndex(l=>l.Codigo).IsUnique();
    }
}