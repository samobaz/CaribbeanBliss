using Caribbean2.Models;
using Microsoft.EntityFrameworkCore;

public class CaribbeanContext : DbContext
{
    public DbSet<Huesped> Huespedes { get; set; }
    public DbSet<HuespedEstado> HuespedEstados { get; set; }
    public DbSet<Servicio> Servicios { get; set; }
    public DbSet<ServicioEstado> ServicioEstados { get; set; }
    public DbSet<Habitacion> Habitaciones { get; set; }
    public DbSet<HabitacionEstado> HabitacionEstados { get; set; }
    public DbSet<Comodidad> Comodidades { get; set; }
    public DbSet<Empleado> Empleados { get; set; }
    public DbSet<Rol> Roles { get; set; }
    public DbSet<Permiso> Permisos { get; set; }
    public DbSet<Suscripcion> Suscripciones { get; set; }
    public DbSet<Metrica> Metricas { get; set; }
    public DbSet<Usuarios> Usuarios { get; set; }

    public CaribbeanContext(DbContextOptions<CaribbeanContext> options)
        : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {

        base.OnModelCreating(modelBuilder);
        // Configuraciones adicionales si es necesario
        modelBuilder.Entity<Usuarios>()
                .HasIndex(u => u.Identificacion)
                .IsUnique();

        modelBuilder.Entity<Usuarios>()
            .HasIndex(u => u.Correo)
            .IsUnique();
        // Suscripcion
        modelBuilder.Entity<Suscripcion>()
            .HasKey(b => b.IdSuscripcion);
        modelBuilder.Entity<Suscripcion>()
            .Property(b => b.IdSuscripcion)
            .ValueGeneratedOnAdd();
        modelBuilder.Entity<Suscripcion>()
            .HasIndex(b => b.Email)
            .IsUnique();
        modelBuilder.Entity<Suscripcion>()
            .Property(s => s.FechaSuscripcion)
            .HasDefaultValueSql("GETDATE()");

        // Empleados
        modelBuilder.Entity<Empleado>()
            .HasKey(e => e.IdEmpleado);
        modelBuilder.Entity<Empleado>()
            .Property(e => e.IdEmpleado)
            .ValueGeneratedOnAdd();
        modelBuilder.Entity<Empleado>()
            .HasIndex(e => e.EmailEmpleado)
            .IsUnique();

        modelBuilder.Entity<Empleado>()
            .HasOne(e => e.Rol)
            .WithMany(r => r.Empleados)
            .HasForeignKey(e => e.RolId)
            .OnDelete(DeleteBehavior.Restrict);

        // Roles
        modelBuilder.Entity<Rol>()
            .HasKey(r => r.IdRol);
        modelBuilder.Entity<Rol>()
            .Property(r => r.IdRol)
            .ValueGeneratedOnAdd();
        modelBuilder.Entity<Rol>()
            .HasIndex(r => r.NombreRol)
            .IsUnique();
        modelBuilder.Entity<Rol>()
            .Property(r => r.DescripcionRol)
            .HasMaxLength(255);

        // Permisos
        modelBuilder.Entity<Permiso>()
            .HasKey(p => p.IdPermiso);
        modelBuilder.Entity<Permiso>()
            .Property(p => p.IdPermiso)
            .ValueGeneratedOnAdd();
        modelBuilder.Entity<Permiso>()
            .HasIndex(p => p.NombrePermiso)
            .IsUnique();

        // Relación muchos a muchos entre Roles y Permisos
        modelBuilder.Entity<Rol>()
            .HasMany(r => r.Permisos)
            .WithMany(p => p.Roles)
            .UsingEntity<Dictionary<string, object>>(
                "RolPermiso",
                r => r.HasOne<Permiso>().WithMany().HasForeignKey("IdPermiso"),
                p => p.HasOne<Rol>().WithMany().HasForeignKey("IdRol")
            );

        // Configuración de la relación 1 a N entre Rol y Empleado
        modelBuilder.Entity<Rol>()
            .HasMany(r => r.Empleados)
            .WithOne(e => e.Rol)
            .HasForeignKey(e => e.RolId)
            .OnDelete(DeleteBehavior.Restrict); // Evita eliminación en cascada de empleados si el rol es eliminado

    }
}
