using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace CCSVWebAPI.Models;

public partial class CcsvdbContext : DbContext
{
    public CcsvdbContext()
    {
    }

    public CcsvdbContext(DbContextOptions<CcsvdbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Cliente> Clientes { get; set; }

    public virtual DbSet<Corte> Cortes { get; set; }

    public virtual DbSet<DatosEmpresa> DatosEmpresas { get; set; }

    public virtual DbSet<DetalleProductoModelo> DetalleProductosModelos { get; set; }

    public virtual DbSet<DetalleProductoOrden> DetalleProductosOrdenes { get; set; }

    public virtual DbSet<DireccionCliente> DireccionesClientes { get; set; }

    public virtual DbSet<EstadoOrden> EstadosOrdenes { get; set; }

    public virtual DbSet<LugarEntrega> LugaresEntregas { get; set; }

    public virtual DbSet<Marca> Marcas { get; set; }

    public virtual DbSet<MedioVenta> MediosVentas { get; set; }

    public virtual DbSet<Modelo> Modelos { get; set; }

    public virtual DbSet<Orden> Ordenes { get; set; }

    public virtual DbSet<Paqueteria> Paqueteria { get; set; }

    public virtual DbSet<Pedido> Pedidos { get; set; }

    public virtual DbSet<PrecioPaqueteria> PreciosPaqueteria { get; set; }

    public virtual DbSet<PrecioProducto> PreciosProductos { get; set; }

    public virtual DbSet<Producto> Productos { get; set; }

    public virtual DbSet<ProductoDefectoPerdida> ProductosDefectosPerdidas { get; set; }

    public virtual DbSet<Proveedor> Proveedores { get; set; }

    public virtual DbSet<TelefonosCliente> TelefonosClientes { get; set; }

    public virtual DbSet<TipoEnvio> TiposEnvios { get; set; }

    public virtual DbSet<TipoPago> TiposPagos { get; set; }

    public virtual DbSet<Venta> Ventas { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {

    }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Cliente>(entity =>
        {
            entity.HasKey(e => e.IdCliente).HasName("PK__Clientes__D5946642DB1CFC79");

            entity.Property(e => e.IdCliente).HasMaxLength(10);
            entity.Property(e => e.NombreCliente).HasMaxLength(100);
        });

        modelBuilder.Entity<Corte>(entity =>
        {
            entity.HasKey(e => e.IdCorte).HasName("PK__Cortes__D491FD4E7EA74C0F");

            entity.Property(e => e.IdCorte).HasMaxLength(10);
            entity.Property(e => e.FechaFinCorte).HasColumnType("date");
            entity.Property(e => e.FechaInicioCorte).HasColumnType("date");
            entity.Property(e => e.TotalGanancias).HasColumnType("decimal(7, 2)");
            entity.Property(e => e.TotalVendido).HasColumnType("decimal(7, 2)");
        });

        modelBuilder.Entity<DatosEmpresa>(entity =>
        {
            entity
                .HasNoKey()
                .ToTable("DatosEmpresa");

            entity.Property(e => e.ColorPrincipal).HasMaxLength(6);
            entity.Property(e => e.ColorSecundario).HasMaxLength(6);
            entity.Property(e => e.ColorTerciario).HasMaxLength(6);
            entity.Property(e => e.NombreEmpresa).HasMaxLength(50);
            entity.Property(e => e.UrlLogoEmpresa).HasMaxLength(200);
        });

        modelBuilder.Entity<DetalleProductoModelo>(entity =>
        {
            entity.HasKey(e => e.IdDetalleProductoModelo).HasName("PK__DetalleP__6B8B45AB351BE8B5");

            entity.Property(e => e.IdModelo).HasMaxLength(10);

            entity.HasOne(d => d.IdModeloNavigation).WithMany(p => p.DetalleProductosModelos)
                .HasForeignKey(d => d.IdModelo)
                .HasConstraintName("FK__DetallePr__IdMod__3D5E1FD2");

            entity.HasOne(d => d.IdPrecioProductoNavigation).WithMany(p => p.DetalleProductosModelos)
                .HasForeignKey(d => d.IdPrecioProducto)
                .HasConstraintName("FK__DetallePr__IdPre__3C69FB99");
        });

        modelBuilder.Entity<DetalleProductoOrden>(entity =>
        {
            entity.HasKey(e => e.IdDetalleProductoOrden).HasName("PK__DetalleP__7FDEC577434DC32D");

            entity.Property(e => e.IdDetalleProductoOrden).ValueGeneratedNever();
            entity.Property(e => e.IdOrden).HasMaxLength(10);

            entity.HasOne(d => d.IdDetalleProductoModeloNavigation).WithMany(p => p.DetalleProductosOrdenes)
                .HasForeignKey(d => d.IdDetalleProductoModelo)
                .HasConstraintName("FK__DetallePr__IdDet__5CD6CB2B");

            entity.HasOne(d => d.IdOrdenNavigation).WithMany(p => p.DetalleProductosOrdenes)
                .HasForeignKey(d => d.IdOrden)
                .HasConstraintName("FK__DetallePr__IdOrd__5BE2A6F2");
        });

        modelBuilder.Entity<DireccionCliente>(entity =>
        {
            entity.HasKey(e => e.IdDireccionCliente).HasName("PK__Direccio__7A8F4C771B1282BB");

            entity.Property(e => e.DepartamentoCliente).HasMaxLength(12);
            entity.Property(e => e.Direccion).HasMaxLength(200);
            entity.Property(e => e.IdCliente).HasMaxLength(10);
            entity.Property(e => e.MunicipioCliente).HasMaxLength(100);

            entity.HasOne(d => d.IdClienteNavigation).WithMany(p => p.DireccionesClientes)
                .HasForeignKey(d => d.IdCliente)
                .HasConstraintName("FK__Direccion__IdCli__4E88ABD4");
        });

        modelBuilder.Entity<EstadoOrden>(entity =>
        {
            entity.HasKey(e => e.IdEstadoOrden).HasName("PK__EstadosO__F2E6940E31D5CD79");

            entity.Property(e => e.IdEstadoOrden).HasMaxLength(5);
            entity.Property(e => e.NombreEstadoOrden).HasMaxLength(50);
        });

        modelBuilder.Entity<LugarEntrega>(entity =>
        {
            entity.HasKey(e => e.IdLugarEntrega).HasName("PK__LugaresE__9BA44CACCBE7CF05");

            entity.Property(e => e.IdLugarEntrega).HasMaxLength(5);
            entity.Property(e => e.Direccion).HasMaxLength(100);
            entity.Property(e => e.NombreLugarEntrega).HasMaxLength(100);
        });

        modelBuilder.Entity<Marca>(entity =>
        {
            entity.HasKey(e => e.IdMarca).HasName("PK__Marcas__4076A8879CE9A17E");

            entity.Property(e => e.IdMarca).HasMaxLength(10);
            entity.Property(e => e.NombreMarca).HasMaxLength(25);
        });

        modelBuilder.Entity<MedioVenta>(entity =>
        {
            entity.HasKey(e => e.IdMedioVenta).HasName("PK__MediosVe__2493C2FD8ADAF928");

            entity.Property(e => e.IdMedioVenta).HasMaxLength(5);
            entity.Property(e => e.NombreMedioVenta).HasMaxLength(50);
        });

        modelBuilder.Entity<Modelo>(entity =>
        {
            entity.HasKey(e => e.IdModelo).HasName("PK__Modelos__CC30D30C556F6A10");

            entity.Property(e => e.IdModelo).HasMaxLength(10);
            entity.Property(e => e.IdMarca).HasMaxLength(10);
            entity.Property(e => e.NombreModelo).HasMaxLength(100);

            entity.HasOne(d => d.IdMarcaNavigation).WithMany(p => p.Modelos)
                .HasForeignKey(d => d.IdMarca)
                .HasConstraintName("FK__Modelos__IdMarca__398D8EEE");
        });

        modelBuilder.Entity<Orden>(entity =>
        {
            entity.HasKey(e => e.IdOrden).HasName("PK__Ordenes__C38F300D76495C27");

            entity.Property(e => e.IdOrden).HasMaxLength(10);
            entity.Property(e => e.ComentariosOrden).HasMaxLength(200);
            entity.Property(e => e.FechaOrden).HasColumnType("date");
            entity.Property(e => e.HoraEntregaOrden).HasMaxLength(200);
            entity.Property(e => e.IdCliente).HasMaxLength(10);
            entity.Property(e => e.IdEstadoOrden).HasMaxLength(5);
            entity.Property(e => e.IdLugarEntrega).HasMaxLength(5);
            entity.Property(e => e.IdMedioVenta).HasMaxLength(5);
            entity.Property(e => e.IdTipoEnvio).HasMaxLength(5);
            entity.Property(e => e.IdTipoPago).HasMaxLength(5);

            entity.HasOne(d => d.IdClienteNavigation).WithMany(p => p.Ordenes)
                .HasForeignKey(d => d.IdCliente)
                .HasConstraintName("FK__Ordenes__IdClien__5441852A");

            entity.HasOne(d => d.IdEstadoOrdenNavigation).WithMany(p => p.Ordenes)
                .HasForeignKey(d => d.IdEstadoOrden)
                .HasConstraintName("FK__Ordenes__IdEstad__5629CD9C");

            entity.HasOne(d => d.IdLugarEntregaNavigation).WithMany(p => p.Ordenes)
                .HasForeignKey(d => d.IdLugarEntrega)
                .HasConstraintName("FK__Ordenes__IdLugar__5812160E");

            entity.HasOne(d => d.IdMedioVentaNavigation).WithMany(p => p.Ordenes)
                .HasForeignKey(d => d.IdMedioVenta)
                .HasConstraintName("FK__Ordenes__IdMedio__5535A963");

            entity.HasOne(d => d.IdTipoEnvioNavigation).WithMany(p => p.Ordenes)
                .HasForeignKey(d => d.IdTipoEnvio)
                .HasConstraintName("FK__Ordenes__IdTipoE__571DF1D5");

            entity.HasOne(d => d.IdTipoPagoNavigation).WithMany(p => p.Ordenes)
                .HasForeignKey(d => d.IdTipoPago)
                .HasConstraintName("FK__Ordenes__IdTipoP__59063A47");
        });

        modelBuilder.Entity<Paqueteria>(entity =>
        {
            entity.HasKey(e => e.IdPaqueteria).HasName("PK__Paqueter__3AC5C7E3B55FE9C6");

            entity.Property(e => e.IdPaqueteria).HasMaxLength(10);
            entity.Property(e => e.IdProveedor).HasMaxLength(10);
            entity.Property(e => e.NombrePaqueteria).HasMaxLength(50);
            entity.Property(e => e.UrlImagenPaqueteria).HasMaxLength(200);

            entity.HasOne(d => d.IdProveedorNavigation).WithMany(p => p.Paqueteria)
                .HasForeignKey(d => d.IdProveedor)
                .HasConstraintName("FK__Paqueteri__IdPro__29572725");
        });

        modelBuilder.Entity<Pedido>(entity =>
        {
            entity.HasKey(e => e.IdPedido).HasName("PK__Pedidos__9D335DC3D1BA3403");

            entity.Property(e => e.IdPedido).HasMaxLength(10);
            entity.Property(e => e.FechaOrdenado).HasColumnType("date");
            entity.Property(e => e.FechaRecibido).HasColumnType("date");
            entity.Property(e => e.IdProveedor).HasMaxLength(10);
            entity.Property(e => e.TotalImportePedido).HasColumnType("decimal(7, 2)");
            entity.Property(e => e.TotalPedido).HasColumnType("decimal(7, 2)");
            entity.Property(e => e.TotalProductosPedido).HasColumnType("decimal(7, 2)");

            entity.HasOne(d => d.Proveedor).WithMany(p => p.Pedidos)
                .HasForeignKey(d => d.IdProveedor)
                .HasConstraintName("FK__Pedidos__IdProve__2F10007B");
        });

        modelBuilder.Entity<PrecioPaqueteria>(entity =>
        {
            entity.HasKey(e => e.IdPrecioPaqueteria).HasName("PK__PreciosP__C7B3DC0EAD0F4E11");

            entity.Property(e => e.CompraTotalPaqueteria).HasColumnType("decimal(7, 2)");
            entity.Property(e => e.FechaCompra).HasColumnType("date");
            entity.Property(e => e.FechaFinUso).HasColumnType("date");
            entity.Property(e => e.FechaInicioUso).HasColumnType("date");
            entity.Property(e => e.IdPaqueteria).HasMaxLength(10);
            entity.Property(e => e.PrecioUnidadPaqueteria).HasColumnType("decimal(7, 2)");

            entity.HasOne(d => d.IdPaqueteriaNavigation).WithMany(p => p.PreciosPaqueteria)
                .HasForeignKey(d => d.IdPaqueteria)
                .HasConstraintName("FK__PreciosPa__IdPaq__2C3393D0");
        });

        modelBuilder.Entity<PrecioProducto>(entity =>
        {
            entity.HasKey(e => e.IdPrecioProducto).HasName("PK__PreciosP__843147DD424F0216");

            entity.Property(e => e.CompraTotalProducto).HasColumnType("decimal(7, 2)");
            entity.Property(e => e.CompraUnidadProducto).HasColumnType("decimal(7, 2)");
            entity.Property(e => e.CompraUnidadPublico).HasColumnType("decimal(7, 2)");
            entity.Property(e => e.Ganancia).HasColumnType("decimal(7, 2)");
            entity.Property(e => e.IdPedido).HasMaxLength(10);
            entity.Property(e => e.IdProducto).HasMaxLength(10);
            entity.Property(e => e.Importacion).HasColumnType("decimal(7, 2)");
            entity.Property(e => e.Paqueteria).HasColumnType("decimal(7, 2)");
            entity.Property(e => e.PrecioPublico).HasColumnType("decimal(7, 2)");
            entity.Property(e => e.TarifaEnvio).HasColumnType("decimal(7, 2)");

            entity.HasOne(d => d.Pedido).WithMany(p => p.PreciosProductos)
                .HasForeignKey(d => d.IdPedido)
                .HasConstraintName("FK__PreciosPr__IdPed__34C8D9D1");

            entity.HasOne(d => d.Producto).WithMany(p => p.PreciosProductos)
                .HasForeignKey(d => d.IdProducto)
                .HasConstraintName("FK__PreciosPr__IdPro__33D4B598");
        });

        modelBuilder.Entity<Producto>(entity =>
        {
            entity.HasKey(e => e.IdProducto).HasName("PK__Producto__0988921006A2BB52");

            entity.Property(e => e.IdProducto).HasMaxLength(10);
            entity.Property(e => e.NombreProducto).HasMaxLength(100);
            entity.Property(e => e.UrlImagenProducto).HasMaxLength(200);
            entity.Property(e => e.UrlProductoProveedor).HasMaxLength(200);
        });

        modelBuilder.Entity<ProductoDefectoPerdida>(entity =>
        {
            entity.HasKey(e => e.IdProductoDefectoPerdida).HasName("PK__Producto__470B131864C2E3C1");

            entity.Property(e => e.TotalPerdido).HasColumnType("decimal(7, 2)");

            entity.HasOne(d => d.IdDetalleProductoModeloNavigation).WithMany(p => p.ProductosDefectosPerdida)
                .HasForeignKey(d => d.IdDetalleProductoModelo)
                .HasConstraintName("FK__Productos__IdDet__403A8C7D");
        });

        modelBuilder.Entity<Proveedor>(entity =>
        {
            entity.HasKey(e => e.IdProveedor).HasName("PK__Proveedo__E8B631AF4807FE13");

            entity.Property(e => e.IdProveedor).HasMaxLength(10);
            entity.Property(e => e.Comentarios).HasMaxLength(200);
            entity.Property(e => e.ContactoProveedor).HasMaxLength(9);
            entity.Property(e => e.NombreProveedor).HasMaxLength(100);
        });

        modelBuilder.Entity<TelefonosCliente>(entity =>
        {
            entity.HasKey(e => e.IdTelefonoCliente).HasName("PK__Telefono__C3797847AF3B214F");

            entity.Property(e => e.IdCliente).HasMaxLength(10);
            entity.Property(e => e.TelefonoCliente).HasMaxLength(9);

            entity.HasOne(d => d.IdClienteNavigation).WithMany(p => p.TelefonosClientes)
                .HasForeignKey(d => d.IdCliente)
                .HasConstraintName("FK__Telefonos__IdCli__5165187F");
        });

        modelBuilder.Entity<TipoEnvio>(entity =>
        {
            entity.HasKey(e => e.IdTipoEnvio).HasName("PK__TiposEnv__29637E72A804443B");

            entity.Property(e => e.IdTipoEnvio).HasMaxLength(5);
            entity.Property(e => e.NombreTipoEnvio).HasMaxLength(100);
        });

        modelBuilder.Entity<TipoPago>(entity =>
        {
            entity.HasKey(e => e.IdTipoPago).HasName("PK__TiposPag__EB0AA9E7F3E29E16");

            entity.Property(e => e.IdTipoPago).HasMaxLength(5);
            entity.Property(e => e.NombreTipoPago).HasMaxLength(50);
        });

        modelBuilder.Entity<Venta>(entity =>
        {
            entity.HasKey(e => e.IdVenta).HasName("PK__Ventas__BC1240BD297E93AE");

            entity.Property(e => e.IdVenta).HasMaxLength(10);
            entity.Property(e => e.CostoGuía).HasColumnType("decimal(7, 2)");
            entity.Property(e => e.CostoTotalVenta).HasColumnType("decimal(7, 2)");
            entity.Property(e => e.FechaVenta).HasColumnType("date");
            entity.Property(e => e.Ganancia).HasColumnType("decimal(7, 2)");
            entity.Property(e => e.IdOrden).HasMaxLength(10);
            entity.Property(e => e.TarifaEnvio).HasColumnType("decimal(7, 2)");
            entity.Property(e => e.VentaTotalFinal).HasColumnType("decimal(7, 2)");

            entity.HasOne(d => d.IdOrdenNavigation).WithMany(p => p.Venta)
                .HasForeignKey(d => d.IdOrden)
                .HasConstraintName("FK__Ventas__IdOrden__5FB337D6");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
