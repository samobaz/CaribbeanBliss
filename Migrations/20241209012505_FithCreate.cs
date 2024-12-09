using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Caribbean2.Migrations
{
    /// <inheritdoc />
    public partial class FithCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ClienteReserva",
                columns: table => new
                {
                    IdClienteReserva = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NombreCliente = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Huespedes = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TipoDocumento = table.Column<string>(type: "nvarchar(3)", maxLength: 3, nullable: false),
                    NumeroDocumento = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    fechaReserva = table.Column<DateOnly>(type: "date", nullable: false),
                    fechaLlegada = table.Column<DateOnly>(type: "date", nullable: false),
                    fechaSalida = table.Column<DateOnly>(type: "date", nullable: false),
                    UsuarioId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IdHabitacion = table.Column<int>(type: "int", nullable: false),
                    TipodeHabitacion = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ClienteReserva", x => x.IdClienteReserva);
                });

            migrationBuilder.CreateTable(
                name: "Consultas",
                columns: table => new
                {
                    IdConsulta = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NombresApellidos = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Email = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Asunto = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    Mensaje = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: false),
                    FechaConsulta = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Consultas", x => x.IdConsulta);
                });

            migrationBuilder.CreateTable(
                name: "HabitacionEstados",
                columns: table => new
                {
                    IdEstado = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nombre = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HabitacionEstados", x => x.IdEstado);
                });

            migrationBuilder.CreateTable(
                name: "HuespedEstados",
                columns: table => new
                {
                    IdEstadoHuesped = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NombreEstado = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HuespedEstados", x => x.IdEstadoHuesped);
                });

            migrationBuilder.CreateTable(
                name: "Metricas",
                columns: table => new
                {
                    IdMetrica = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Fecha = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IngresosTotales = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    TasaOcupacion = table.Column<double>(type: "float", nullable: false),
                    OcupacionDiaria = table.Column<int>(type: "int", nullable: false),
                    OcupacionSemanal = table.Column<int>(type: "int", nullable: false),
                    OcupacionMensual = table.Column<int>(type: "int", nullable: false),
                    ReservasNuevas = table.Column<int>(type: "int", nullable: false),
                    Cancelaciones = table.Column<int>(type: "int", nullable: false),
                    ImpactoFinancieroCancelaciones = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    PromedioDiasAnticipacionReserva = table.Column<double>(type: "float", nullable: false),
                    NumeroHuespedes = table.Column<int>(type: "int", nullable: false),
                    SuscritosBoletin = table.Column<int>(type: "int", nullable: false),
                    DuracionPromedioEstadia = table.Column<double>(type: "float", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Metricas", x => x.IdMetrica);
                });

            migrationBuilder.CreateTable(
                name: "Permisos",
                columns: table => new
                {
                    IdPermiso = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NombrePermiso = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    DescripcionPermiso = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Permisos", x => x.IdPermiso);
                });

            migrationBuilder.CreateTable(
                name: "ReservaEstados",
                columns: table => new
                {
                    IdEstado = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nombre = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ReservaEstados", x => x.IdEstado);
                });

            migrationBuilder.CreateTable(
                name: "Roles",
                columns: table => new
                {
                    IdRol = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NombreRol = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    DescripcionRol = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    EstadoRol = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Roles", x => x.IdRol);
                });

            migrationBuilder.CreateTable(
                name: "Servicios",
                columns: table => new
                {
                    IdServicio = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nombre = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    Descripcion = table.Column<string>(type: "nvarchar(300)", maxLength: 300, nullable: true),
                    PrecioServicio = table.Column<decimal>(type: "money", nullable: false),
                    EstadoServicio = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Servicios", x => x.IdServicio);
                });

            migrationBuilder.CreateTable(
                name: "Suscripciones",
                columns: table => new
                {
                    IdSuscripcion = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nombre = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Email = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    FechaSuscripcion = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GETDATE()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Suscripciones", x => x.IdSuscripcion);
                });

            migrationBuilder.CreateTable(
                name: "Habitaciones",
                columns: table => new
                {
                    IdHabitacion = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nombre = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    Descripcion = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    Capacidad = table.Column<int>(type: "int", nullable: false),
                    NumeroHabitacion = table.Column<int>(type: "int", nullable: false),
                    PrecioHabitacion = table.Column<decimal>(type: "money", nullable: false),
                    HabitacionesDisponibles = table.Column<int>(type: "int", nullable: false),
                    IdEstado = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Habitaciones", x => x.IdHabitacion);
                    table.ForeignKey(
                        name: "FK_Habitaciones_HabitacionEstados_IdEstado",
                        column: x => x.IdEstado,
                        principalTable: "HabitacionEstados",
                        principalColumn: "IdEstado",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Huespedes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NombreCompleto = table.Column<string>(type: "nvarchar(70)", maxLength: 70, nullable: false),
                    CorreoElectronico = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    TipoDocumento = table.Column<string>(type: "nvarchar(3)", maxLength: 3, nullable: false),
                    NumeroIdentificacion = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    NumeroContacto = table.Column<string>(type: "nvarchar(15)", maxLength: 15, nullable: true),
                    LugarResidencia = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    FechaLlegada = table.Column<DateTime>(type: "datetime2", nullable: false),
                    FechaSalida = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IdEstadoHuesped = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Huespedes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Huespedes_HuespedEstados_IdEstadoHuesped",
                        column: x => x.IdEstadoHuesped,
                        principalTable: "HuespedEstados",
                        principalColumn: "IdEstadoHuesped");
                });

            migrationBuilder.CreateTable(
                name: "Clientes",
                columns: table => new
                {
                    idCliente = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    nombre = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    email = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    telefono = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClienteEstado = table.Column<bool>(type: "bit", nullable: false),
                    idRol = table.Column<int>(type: "int", nullable: false),
                    idRolNavigationIdRol = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Clientes", x => x.idCliente);
                    table.ForeignKey(
                        name: "FK_Clientes_Roles_idRolNavigationIdRol",
                        column: x => x.idRolNavigationIdRol,
                        principalTable: "Roles",
                        principalColumn: "IdRol");
                });

            migrationBuilder.CreateTable(
                name: "Empleados",
                columns: table => new
                {
                    IdEmpleado = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NombreEmpleado = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    EmailEmpleado = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    EstadoEmpleado = table.Column<bool>(type: "bit", nullable: false),
                    RolId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Empleados", x => x.IdEmpleado);
                    table.ForeignKey(
                        name: "FK_Empleados_Roles_RolId",
                        column: x => x.RolId,
                        principalTable: "Roles",
                        principalColumn: "IdRol",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "RolPermiso",
                columns: table => new
                {
                    IdPermiso = table.Column<int>(type: "int", nullable: false),
                    IdRol = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RolPermiso", x => new { x.IdPermiso, x.IdRol });
                    table.ForeignKey(
                        name: "FK_RolPermiso_Permisos_IdPermiso",
                        column: x => x.IdPermiso,
                        principalTable: "Permisos",
                        principalColumn: "IdPermiso",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_RolPermiso_Roles_IdRol",
                        column: x => x.IdRol,
                        principalTable: "Roles",
                        principalColumn: "IdRol",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Usuarios",
                columns: table => new
                {
                    UsuarioID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NombresApellidos = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
                    TipoIdentificacion = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Identificacion = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Telefono = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    Correo = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    Contrasena = table.Column<string>(type: "nvarchar(15)", maxLength: 15, nullable: false),
                    FechaRegistro = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Estado = table.Column<bool>(type: "bit", nullable: false),
                    IdRol = table.Column<int>(type: "int", nullable: false, defaultValue: 1),
                    RolIdRol = table.Column<int>(type: "int", nullable: true),
                    ResetPasswordToken = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ResetPasswordExpiry = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Usuarios", x => x.UsuarioID);
                    table.ForeignKey(
                        name: "FK_Usuarios_Roles_IdRol",
                        column: x => x.IdRol,
                        principalTable: "Roles",
                        principalColumn: "IdRol",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Usuarios_Roles_RolIdRol",
                        column: x => x.RolIdRol,
                        principalTable: "Roles",
                        principalColumn: "IdRol");
                });

            migrationBuilder.CreateTable(
                name: "Reservas",
                columns: table => new
                {
                    IdReserva = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IdCliente = table.Column<int>(type: "int", nullable: false),
                    IdHabitacion = table.Column<int>(type: "int", nullable: false),
                    NumeroHabitacion = table.Column<int>(type: "int", nullable: false),
                    FechaInicio = table.Column<DateTime>(type: "datetime2", nullable: false),
                    FechaFin = table.Column<DateTime>(type: "datetime2", nullable: false),
                    NumeroPersonas = table.Column<int>(type: "int", nullable: false),
                    PrecioTotal = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Anticipo = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Notas = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IdEstado = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Reservas", x => x.IdReserva);
                    table.ForeignKey(
                        name: "FK_Reservas_Clientes_IdCliente",
                        column: x => x.IdCliente,
                        principalTable: "Clientes",
                        principalColumn: "idCliente",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Reservas_Habitaciones_IdHabitacion",
                        column: x => x.IdHabitacion,
                        principalTable: "Habitaciones",
                        principalColumn: "IdHabitacion",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Reservas_ReservaEstados_IdEstado",
                        column: x => x.IdEstado,
                        principalTable: "ReservaEstados",
                        principalColumn: "IdEstado",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Calificaciones",
                columns: table => new
                {
                    IdCalificacion = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Puntuacion = table.Column<int>(type: "int", nullable: false),
                    Comentario = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    IdReserva = table.Column<int>(type: "int", nullable: false),
                    IdCliente = table.Column<int>(type: "int", nullable: false),
                    FechaCalificacion = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GETDATE()"),
                    EstadoCalificacion = table.Column<bool>(type: "bit", nullable: false, defaultValue: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Calificaciones", x => x.IdCalificacion);
                    table.ForeignKey(
                        name: "FK_Calificaciones_Clientes_IdCliente",
                        column: x => x.IdCliente,
                        principalTable: "Clientes",
                        principalColumn: "idCliente",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Calificaciones_Reservas_IdReserva",
                        column: x => x.IdReserva,
                        principalTable: "Reservas",
                        principalColumn: "IdReserva",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Pagos",
                columns: table => new
                {
                    IdPago = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TipoPago = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Fecha = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Valor = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    IdReserva = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Pagos", x => x.IdPago);
                    table.ForeignKey(
                        name: "FK_Pagos_Reservas_IdReserva",
                        column: x => x.IdReserva,
                        principalTable: "Reservas",
                        principalColumn: "IdReserva",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ReservaHuesped",
                columns: table => new
                {
                    HuespedesId = table.Column<int>(type: "int", nullable: false),
                    ReservasIdReserva = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ReservaHuesped", x => new { x.HuespedesId, x.ReservasIdReserva });
                    table.ForeignKey(
                        name: "FK_ReservaHuesped_Huespedes_HuespedesId",
                        column: x => x.HuespedesId,
                        principalTable: "Huespedes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ReservaHuesped_Reservas_ReservasIdReserva",
                        column: x => x.ReservasIdReserva,
                        principalTable: "Reservas",
                        principalColumn: "IdReserva",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ReservaServicio",
                columns: table => new
                {
                    ReservasIdReserva = table.Column<int>(type: "int", nullable: false),
                    ServiciosIdServicio = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ReservaServicio", x => new { x.ReservasIdReserva, x.ServiciosIdServicio });
                    table.ForeignKey(
                        name: "FK_ReservaServicio_Reservas_ReservasIdReserva",
                        column: x => x.ReservasIdReserva,
                        principalTable: "Reservas",
                        principalColumn: "IdReserva",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ReservaServicio_Servicios_ServiciosIdServicio",
                        column: x => x.ServiciosIdServicio,
                        principalTable: "Servicios",
                        principalColumn: "IdServicio",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "HabitacionEstados",
                columns: new[] { "IdEstado", "Nombre" },
                values: new object[,]
                {
                    { 1, "Disponible" },
                    { 2, "Ocupada" },
                    { 3, "Inhabilitada" }
                });

            migrationBuilder.InsertData(
                table: "HuespedEstados",
                columns: new[] { "IdEstadoHuesped", "NombreEstado" },
                values: new object[,]
                {
                    { 1, "Activo" },
                    { 3, "Proceso Check-Out" },
                    { 4, "Check-Out Completado" },
                    { 5, "Cancelado" },
                    { 6, "Aplazado" },
                    { 7, "Suspendido" }
                });

            migrationBuilder.InsertData(
                table: "ReservaEstados",
                columns: new[] { "IdEstado", "Nombre" },
                values: new object[,]
                {
                    { 1, "En Pendiente" },
                    { 2, "Confirmada" },
                    { 3, "En Progreso" },
                    { 4, "Completada" },
                    { 5, "Cancelada" }
                });

            migrationBuilder.InsertData(
                table: "Roles",
                columns: new[] { "IdRol", "DescripcionRol", "EstadoRol", "NombreRol" },
                values: new object[,]
                {
                    { 1, "Cliente", true, "Cliente" },
                    { 2, "Empleado", false, "Empleado" },
                    { 3, "Administrador", true, "Administrador" },
                    { 4, "Gerente", false, "Gerente" }
                });

            migrationBuilder.InsertData(
                table: "Servicios",
                columns: new[] { "IdServicio", "Descripcion", "EstadoServicio", "Nombre", "PrecioServicio" },
                values: new object[,]
                {
                    { 1, "Vive la emoción del paracaidismo con un salto en tándem. Disfruta de vistas impresionantes y la adrenalina de caer en caída libre, acompañado de un instructor experto. Un servicio único para quienes buscan una experiencia extrema y memorable, ¡haz realidad tu sueño de volar!", true, "Paracaidismo", 99.99m },
                    { 5, "Recibe asesoramiento experto con nuestras guías de viaje personalizadas que te ayudarán a descubrir lo mejor del Caribe.", true, "Viaje Personalizadas", 69.99m },
                    { 8, "Viaja con comodidad en nuestros vehículos de lujo, equipados para ofrecerte la mejor experiencia de transporte.", true, "Transporte de Lujo", 49.99m },
                    { 9, "Diviértete en nuestro parque acuático, con emocionantes toboganes, piscinas de olas y zonas de relajación. Ideal para toda la familia, ofrece atracciones para todos los gustos, desde aventuras acuáticas hasta momentos de descanso. Ven y disfruta de un día lleno de diversión y frescura bajo el sol", true, "Parque acuático", 119.00m },
                    { 10, "Relájate y rejuvenece con nuestros servicios de spa de primera clase, diseñados para ofrecerte una experiencia de bienestar.", true, "Servicios de Spa", 39.99m },
                    { 11, "Saborea una selección de platos gourmet preparados por chefs expertos, con ingredientes frescos y locales.", true, "Cenas Gourmet", 59.99m }
                });

            migrationBuilder.InsertData(
                table: "Habitaciones",
                columns: new[] { "IdHabitacion", "Capacidad", "Descripcion", "HabitacionesDisponibles", "IdEstado", "Nombre", "NumeroHabitacion", "PrecioHabitacion" },
                values: new object[,]
                {
                    { 1, 2, "Habitación Deluxe con diseño moderno, equipada con comodidades premium para una experiencia única de confort.", 1, 1, "Deluxe", 0, 359.99m },
                    { 2, 8, "Habitación ideal para familias, amplia y cómoda, con capacidad para grupos grandes y servicios adaptados a sus necesidades.", 1, 1, "Familiar", 0, 239.99m },
                    { 3, 1, "Habitación perfecta para una sola persona, diseñada para garantizar privacidad y un espacio acogedor.", 1, 1, "Individual", 0, 119.99m },
                    { 4, 2, "Habitación VIP con servicios exclusivos, lujo excepcional y diseño elegante para huéspedes exigentes.", 1, 1, "VIP", 0, 539.99m }
                });

            migrationBuilder.InsertData(
                table: "Usuarios",
                columns: new[] { "UsuarioID", "Contrasena", "Correo", "Estado", "FechaRegistro", "IdRol", "Identificacion", "NombresApellidos", "ResetPasswordExpiry", "ResetPasswordToken", "RolIdRol", "Telefono", "TipoIdentificacion" },
                values: new object[] { 1, "nimad4321", "admin@admincorreo.com", true, new DateTime(2024, 12, 8, 20, 25, 2, 686, DateTimeKind.Local).AddTicks(218), 3, "1", "admin", null, null, null, "1", "CC" });

            migrationBuilder.CreateIndex(
                name: "IX_Calificaciones_IdCliente",
                table: "Calificaciones",
                column: "IdCliente");

            migrationBuilder.CreateIndex(
                name: "IX_Calificaciones_IdReserva",
                table: "Calificaciones",
                column: "IdReserva");

            migrationBuilder.CreateIndex(
                name: "IX_Clientes_idRolNavigationIdRol",
                table: "Clientes",
                column: "idRolNavigationIdRol");

            migrationBuilder.CreateIndex(
                name: "IX_Empleados_EmailEmpleado",
                table: "Empleados",
                column: "EmailEmpleado",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Empleados_RolId",
                table: "Empleados",
                column: "RolId");

            migrationBuilder.CreateIndex(
                name: "IX_Habitaciones_IdEstado",
                table: "Habitaciones",
                column: "IdEstado");

            migrationBuilder.CreateIndex(
                name: "IX_Huespedes_IdEstadoHuesped",
                table: "Huespedes",
                column: "IdEstadoHuesped");

            migrationBuilder.CreateIndex(
                name: "IX_Pagos_IdReserva",
                table: "Pagos",
                column: "IdReserva");

            migrationBuilder.CreateIndex(
                name: "IX_Permisos_NombrePermiso",
                table: "Permisos",
                column: "NombrePermiso",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_ReservaHuesped_ReservasIdReserva",
                table: "ReservaHuesped",
                column: "ReservasIdReserva");

            migrationBuilder.CreateIndex(
                name: "IX_Reservas_IdCliente",
                table: "Reservas",
                column: "IdCliente");

            migrationBuilder.CreateIndex(
                name: "IX_Reservas_IdEstado",
                table: "Reservas",
                column: "IdEstado");

            migrationBuilder.CreateIndex(
                name: "IX_Reservas_IdHabitacion",
                table: "Reservas",
                column: "IdHabitacion");

            migrationBuilder.CreateIndex(
                name: "IX_ReservaServicio_ServiciosIdServicio",
                table: "ReservaServicio",
                column: "ServiciosIdServicio");

            migrationBuilder.CreateIndex(
                name: "IX_Roles_NombreRol",
                table: "Roles",
                column: "NombreRol",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_RolPermiso_IdRol",
                table: "RolPermiso",
                column: "IdRol");

            migrationBuilder.CreateIndex(
                name: "IX_Suscripciones_Email",
                table: "Suscripciones",
                column: "Email",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Usuarios_Correo",
                table: "Usuarios",
                column: "Correo",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Usuarios_Identificacion",
                table: "Usuarios",
                column: "Identificacion",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Usuarios_IdRol",
                table: "Usuarios",
                column: "IdRol");

            migrationBuilder.CreateIndex(
                name: "IX_Usuarios_RolIdRol",
                table: "Usuarios",
                column: "RolIdRol");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Calificaciones");

            migrationBuilder.DropTable(
                name: "ClienteReserva");

            migrationBuilder.DropTable(
                name: "Consultas");

            migrationBuilder.DropTable(
                name: "Empleados");

            migrationBuilder.DropTable(
                name: "Metricas");

            migrationBuilder.DropTable(
                name: "Pagos");

            migrationBuilder.DropTable(
                name: "ReservaHuesped");

            migrationBuilder.DropTable(
                name: "ReservaServicio");

            migrationBuilder.DropTable(
                name: "RolPermiso");

            migrationBuilder.DropTable(
                name: "Suscripciones");

            migrationBuilder.DropTable(
                name: "Usuarios");

            migrationBuilder.DropTable(
                name: "Huespedes");

            migrationBuilder.DropTable(
                name: "Reservas");

            migrationBuilder.DropTable(
                name: "Servicios");

            migrationBuilder.DropTable(
                name: "Permisos");

            migrationBuilder.DropTable(
                name: "HuespedEstados");

            migrationBuilder.DropTable(
                name: "Clientes");

            migrationBuilder.DropTable(
                name: "Habitaciones");

            migrationBuilder.DropTable(
                name: "ReservaEstados");

            migrationBuilder.DropTable(
                name: "Roles");

            migrationBuilder.DropTable(
                name: "HabitacionEstados");
        }
    }
}
