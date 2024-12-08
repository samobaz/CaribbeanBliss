using Caribbean2.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;

public class CaribbeanContext : DbContext
{
    public DbSet<Huesped> Huespedes { get; set; }
    public DbSet<HuespedEstado> HuespedEstados { get; set; }
    public DbSet<Servicio> Servicios { get; set; }
    public DbSet<Habitacion> Habitaciones { get; set; }
    public DbSet<HabitacionEstado> HabitacionEstados { get; set; }
    public DbSet<Empleado> Empleados { get; set; }
    public DbSet<Rol> Roles { get; set; }
    public DbSet<Permiso> Permisos { get; set; }
    public DbSet<Suscripcion> Suscripciones { get; set; }
    public DbSet<Metrica> Metricas { get; set; }
    public DbSet<Usuarios> Usuarios { get; set; }
    public DbSet<Consulta> Consultas { get; set; }
    public DbSet<Cliente> Clientes { get; set; }
    public DbSet<ClienteReserva> ClienteReservas { get; set; }
    public DbSet<Reserva> Reservas { get; set; }
    public DbSet<ReservaEstado> ReservaEstados { get; set; }
    public DbSet<Pago> Pagos { get; set; }
    public DbSet<Calificacion> Calificaciones { get; set; } // Agregar DbSet para Calificaciones

    public CaribbeanContext(DbContextOptions<CaribbeanContext> options)
        : base(options)
    {
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
       optionsBuilder.ConfigureWarnings(warnings =>
           warnings.Ignore(RelationalEventId.PendingModelChangesWarning));
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Rol>().HasData(
            new Rol { IdRol = 1, NombreRol = "Cliente", DescripcionRol = "Cliente", EstadoRol = true },
            new Rol { IdRol = 2, NombreRol = "Empleado", DescripcionRol = "Empleado", EstadoRol = false },
            new Rol { IdRol = 3, NombreRol = "Administrador", DescripcionRol = "Administrador", EstadoRol = true },
            new Rol { IdRol = 4, NombreRol = "Gerente", DescripcionRol = "Gerente", EstadoRol = false }
        );

        // Usuario administrador predeterminado
        modelBuilder.Entity<Usuarios>().HasData(
            new Usuarios
            {
                UsuarioID = 1,
                NombresApellidos = "admin",
                TipoIdentificacion = "CC",
                Identificacion = "1",
                Telefono = "1",
                Correo = "admin@admincorreo.com",
                Contrasena = "nimad4321",
                FechaRegistro = DateTime.Now,
                Estado = true,
                IdRol = 3
            }
        );
        
        // Servicios predeterminados
        modelBuilder.Entity<Servicio>().HasData(
            new Servicio 
            { 
                IdServicio = 1, 
                Nombre = "Paracaidismo", 
                Descripcion = "Vive la emoción del paracaidismo con un salto en tándem. Disfruta de vistas impresionantes y la adrenalina de caer en caída libre, acompañado de un instructor experto. Un servicio único para quienes buscan una experiencia extrema y memorable, ¡haz realidad tu sueño de volar!", 
                PrecioServicio = 99.99m, 
                EstadoServicio = true 
            },
            new Servicio 
            { 
                IdServicio = 5, 
                Nombre = "Viaje Personalizadas", 
                Descripcion = "Recibe asesoramiento experto con nuestras guías de viaje personalizadas que te ayudarán a descubrir lo mejor del Caribe.", 
                PrecioServicio = 69.99m, 
                EstadoServicio = true 
            },
            new Servicio 
            { 
                IdServicio = 8, 
                Nombre = "Transporte de Lujo", 
                Descripcion = "Viaja con comodidad en nuestros vehículos de lujo, equipados para ofrecerte la mejor experiencia de transporte.", 
                PrecioServicio = 49.99m, 
                EstadoServicio = true 
            },
            new Servicio 
            { 
                IdServicio = 9, 
                Nombre = "Parque acuático", 
                Descripcion = "Diviértete en nuestro parque acuático, con emocionantes toboganes, piscinas de olas y zonas de relajación. Ideal para toda la familia, ofrece atracciones para todos los gustos, desde aventuras acuáticas hasta momentos de descanso. Ven y disfruta de un día lleno de diversión y frescura bajo el sol", 
                PrecioServicio = 119.00m, 
                EstadoServicio = true 
            },
            new Servicio 
            { 
                IdServicio = 10, 
                Nombre = "Servicios de Spa", 
                Descripcion = "Relájate y rejuvenece con nuestros servicios de spa de primera clase, diseñados para ofrecerte una experiencia de bienestar.", 
                PrecioServicio = 39.99m, 
                EstadoServicio = true 
            },
            new Servicio 
            { 
                IdServicio = 11, 
                Nombre = "Cenas Gourmet", 
                Descripcion = "Saborea una selección de platos gourmet preparados por chefs expertos, con ingredientes frescos y locales.", 
                PrecioServicio = 59.99m, 
                EstadoServicio = true 
            }
        );

        // Habitaciones predeterminadas
        modelBuilder.Entity<Habitacion>().HasData(
            new Habitacion 
            { 
                IdHabitacion = 1, 
                Nombre = "Deluxe", 
                Descripcion = "Habitación Deluxe con diseño moderno, equipada con comodidades premium para una experiencia única de confort.", 
                Capacidad = 2, 
                PrecioHabitacion = 359.99m, 
                IdEstado = 1 
            },
            new Habitacion 
            { 
                IdHabitacion = 2, 
                Nombre = "Familiar", 
                Descripcion = "Habitación ideal para familias, amplia y cómoda, con capacidad para grupos grandes y servicios adaptados a sus necesidades.", 
                Capacidad = 8, 
                PrecioHabitacion = 239.99m, 
                IdEstado = 1 
            },
            new Habitacion 
            { 
                IdHabitacion = 3, 
                Nombre = "Individual", 
                Descripcion = "Habitación perfecta para una sola persona, diseñada para garantizar privacidad y un espacio acogedor.", 
                Capacidad = 1, 
                PrecioHabitacion = 119.99m, 
                IdEstado = 1 
            },
            new Habitacion 
            { 
                IdHabitacion = 4, 
                Nombre = "VIP", 
                Descripcion = "Habitación VIP con servicios exclusivos, lujo excepcional y diseño elegante para huéspedes exigentes.", 
                Capacidad = 2, 
                PrecioHabitacion = 539.99m, 
                IdEstado = 1 
            }
        );

        // Estados de habitaciones predeterminados
        modelBuilder.Entity<HabitacionEstado>().HasData(
            new HabitacionEstado 
            { 
                IdEstado = 1, 
                Nombre = "Disponible" 
            },
            new HabitacionEstado 
            { 
                IdEstado = 2, 
                Nombre = "Ocupada" 
            },
            new HabitacionEstado 
            { 
                IdEstado = 3, 
                Nombre = "Inhabilitada" 
            }
        );

        // Estados de Huespedes predeterminados
        modelBuilder.Entity<HuespedEstado>().HasData(
            new HuespedEstado 
            { 
                IdEstadoHuesped = 1, 
                NombreEstado = "Activo" 
            },
            new HuespedEstado 
            { 
                IdEstadoHuesped = 3, 
                NombreEstado = "Proceso Check-Out" 
            },
            new HuespedEstado 
            { 
                IdEstadoHuesped = 4, 
                NombreEstado = "Check-Out Completado" 
            },
            new HuespedEstado 
            { 
                IdEstadoHuesped = 5, 
                NombreEstado = "Cancelado" 
            },
            new HuespedEstado 
            { 
                IdEstadoHuesped = 6, 
                NombreEstado = "Aplazado" 
            },
            new HuespedEstado 
            { 
                IdEstadoHuesped = 7, 
                NombreEstado = "Suspendido" 
            }
        );

        modelBuilder.Entity<ReservaEstado>().HasData(
            new ReservaEstado 
            { 
                IdEstado = 1, 
                Nombre = "En Pendiente" 
            },
            new ReservaEstado 
            { 
                IdEstado = 2, 
                Nombre = "Confirmada" 
            },
            new ReservaEstado 
            { 
                IdEstado = 3, 
                Nombre = "En Progreso" 
            },
            new ReservaEstado 
            { 
                IdEstado = 4, 
                Nombre = "Completada" 
            },
            new ReservaEstado 
            { 
                IdEstado = 5, 
                Nombre = "Cancelada" 
            }
        );

        modelBuilder.Entity<Reserva>()
            .HasOne(r => r.Cliente)
            .WithMany(c => c.Reservas)
            .HasForeignKey(r => r.IdCliente);

        modelBuilder.Entity<Reserva>()
            .HasOne(r => r.Habitacion)
            .WithMany(h => h.Reservas)
            .HasForeignKey(r => r.IdHabitacion);

        modelBuilder.Entity<Reserva>()
            .HasMany(r => r.Servicios)
            .WithMany(s => s.Reservas)
            .UsingEntity(j => j.ToTable("ReservaServicio"));

        modelBuilder.Entity<Reserva>()
            .HasOne(r => r.Estado)
            .WithMany(e => e.Reservas)
            .HasForeignKey(r => r.IdEstado)
            .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<Reserva>()
            .HasMany(r => r.Huespedes)
            .WithMany(h => h.Reservas)
            .UsingEntity(j => j.ToTable("ReservaHuesped"));

        modelBuilder.Entity<Reserva>()
            .HasMany(r => r.Pagos)
            .WithOne(p => p.Reserva)
            .HasForeignKey(p => p.IdReserva);

        modelBuilder.Entity<Reserva>()
            .HasMany(r => r.Pagos)
            .WithOne(p => p.Reserva)
            .HasForeignKey(p => p.IdReserva)
            .OnDelete(DeleteBehavior.Restrict);

        // Configuración de Calificaciones
        modelBuilder.Entity<Calificacion>()
            .HasKey(c => c.IdCalificacion);

        modelBuilder.Entity<Calificacion>()
            .Property(c => c.IdCalificacion)
            .ValueGeneratedOnAdd();

        // Relación con Reserva
        modelBuilder.Entity<Calificacion>()
            .HasOne(c => c.Reserva)
            .WithMany(r => r.Calificaciones)
            .HasForeignKey(c => c.IdReserva)
            .OnDelete(DeleteBehavior.Restrict);

        // Relación con Cliente
        modelBuilder.Entity<Calificacion>()
            .HasOne(c => c.Cliente)
            .WithMany()
            .HasForeignKey(c => c.IdCliente)
            .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<Calificacion>()
            .Property(c => c.FechaCalificacion)
            .HasDefaultValueSql("GETDATE()");

        modelBuilder.Entity<Calificacion>()
            .Property(c => c.EstadoCalificacion)
            .HasDefaultValue(true);

        // Configuraciones adicionales si es necesario
        modelBuilder.Entity<Usuarios>()
                .HasIndex(u => u.Identificacion)
                .IsUnique();

        modelBuilder.Entity<Usuarios>()
        .HasKey(u => u.UsuarioID);

        modelBuilder.Entity<Usuarios>()
            .HasIndex(u => u.Correo)
            .IsUnique();

        modelBuilder.Entity<Usuarios>()
            .Property(u => u.Correo)
            .IsRequired()
            .HasMaxLength(255);

        modelBuilder.Entity<Usuarios>()
            .Property(u => u.Contrasena)
            .IsRequired();
        
        modelBuilder.Entity<Usuarios>()
            .HasIndex(u => u.Correo)
            .IsUnique();

        modelBuilder.Entity<Usuarios>()
            .HasOne(u => u.Rol)
            .WithMany(r => r.Usuarios) 
            .HasForeignKey(u => u.IdRol)
            .OnDelete(DeleteBehavior.Restrict);
            
        // Configuración de la relación entre Usuario y Rol
        modelBuilder.Entity<Usuarios>()
            .HasOne<Rol>()
            .WithMany()
            .HasForeignKey(u => u.IdRol)
            .OnDelete(DeleteBehavior.Restrict)
            .IsRequired(); // Hace que IdRol sea requerido

        modelBuilder.Entity<Usuarios>()
            .Property(u => u.IdRol)
            .HasDefaultValue(1); // Establece 1 como valor predeterminado

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

        base.OnModelCreating(modelBuilder);
    }

    public DbSet<Caribbean2.Models.ClienteReserva> ClienteReserva { get; set; }
}
