using System;
using System.Collections.Generic;
using BibliotecaSys.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace BibliotecaSys.Infrastructure.Data;

public partial class AppDbContext : DbContext
{
    public AppDbContext()
    {
    }

    public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Estado> Estados { get; set; }

    public virtual DbSet<HistorialPrestamo> HistorialPrestamos { get; set; }

    public virtual DbSet<Libro> Libros { get; set; }

    public virtual DbSet<Notificacione> Notificaciones { get; set; }

    public virtual DbSet<Prestamo> Prestamos { get; set; }

    public virtual DbSet<Reserva> Reservas { get; set; }

    public virtual DbSet<Usuario> Usuarios { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Estado>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Estados__3214EC07F59919A0");

            entity.Property(e => e.Estado1)
                .HasMaxLength(20)
                .HasColumnName("Estado");

            entity.HasData(
                new Estado { Id = 1, Estado1 = "Disponible" },
                new Estado { Id = 2, Estado1 = "Reservado" },
                new Estado { Id = 3, Estado1 = "Prestado" },
                new Estado { Id = 4, Estado1 = "Devuelto" },
                new Estado { Id = 5, Estado1 = "Leído" },
                new Estado { Id = 6, Estado1 = "No Leído" }
            );
        });

        modelBuilder.Entity<HistorialPrestamo>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Historia__3214EC07526E10EC");

            entity.Property(e => e.FechaDevolucion).HasColumnType("datetime");
            entity.Property(e => e.FechaPrestamo).HasColumnType("datetime");

            entity.HasOne(d => d.IdLibroNavigation).WithMany(p => p.HistorialPrestamos)
                .HasForeignKey(d => d.IdLibro)
                .HasConstraintName("FK__Historial__IdLib__48CFD27E");

            entity.HasOne(d => d.IdUsuarioNavigation).WithMany(p => p.HistorialPrestamos)
                .HasForeignKey(d => d.IdUsuario)
                .HasConstraintName("FK__Historial__IdUsu__47DBAE45");
        });

        modelBuilder.Entity<Libro>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Libros__3214EC073F836FEF");

            entity.Property(e => e.Autor).HasMaxLength(100);
            entity.Property(e => e.Genero).HasMaxLength(100);
            entity.Property(e => e.IdEstado).HasDefaultValueSql("((1))");
            entity.Property(e => e.Titulo).HasMaxLength(200);

            entity.HasOne(d => d.IdEstadoNavigation).WithMany(p => p.Libros)
                .HasForeignKey(d => d.IdEstado)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Libros__IdEstado__3C69FB99");

            entity.HasData(
            new Libro { Id = 1, Titulo = "Don Quijote de la Mancha", Autor = "Miguel de Cervantes Saavedra", Genero = "Novela de caballerías", IdEstado = 1 },
            new Libro { Id = 2, Titulo = "Cien años de soledad", Autor = "Gabriel García Márquez", Genero = "Realismo mágico", IdEstado = 1 },
            new Libro { Id = 3, Titulo = "1984", Autor = "George Orwell", Genero = "Ficción distópica", IdEstado = 1 },
            new Libro { Id = 4, Titulo = "Orgullo y prejuicio", Autor = "Jane Austen", Genero = "Novela romántica", IdEstado = 1 },
            new Libro { Id = 5, Titulo = "Moby-Dick", Autor = "Herman Melville", Genero = "Novela de aventuras", IdEstado = 1 },
            new Libro { Id = 6, Titulo = "Ulises", Autor = "James Joyce", Genero = "Novela modernista", IdEstado = 1 },
            new Libro { Id = 7, Titulo = "En busca del tiempo perdido", Autor = "Marcel Proust", Genero = "Novela introspectiva", IdEstado = 1 },
            new Libro { Id = 8, Titulo = "Crimen y castigo", Autor = "Fyodor Dostoevsky", Genero = "Novela psicológica", IdEstado = 1 },
            new Libro { Id = 9, Titulo = "El gran Gatsby", Autor = "F. Scott Fitzgerald", Genero = "Novela del jazz", IdEstado = 1 },
            new Libro { Id = 10, Titulo = "Donde los árboles cantan", Autor = "Laura Gallego García", Genero = "Fantasía", IdEstado = 1 },
            new Libro { Id = 11, Titulo = "El nombre del viento", Autor = "Patrick Rothfuss", Genero = "Fantasía épica", IdEstado = 1 },
            new Libro { Id = 12, Titulo = "El señor de los anillos", Autor = "J.R.R. Tolkien", Genero = "Fantasía épica", IdEstado = 1 },
            new Libro { Id = 13, Titulo = "Harry Potter y la piedra filosofal", Autor = "J.K. Rowling", Genero = "Fantasía juvenil", IdEstado = 1 },
            new Libro { Id = 14, Titulo = "Los juegos del hambre", Autor = "Suzanne Collins", Genero = "Ciencia ficción", IdEstado = 1 },
            new Libro { Id = 15, Titulo = "El código Da Vinci", Autor = "Dan Brown", Genero = "Thriller", IdEstado = 1 });
        });

        modelBuilder.Entity<Notificacione>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Notifica__3214EC07B777F735");

            entity.Property(e => e.FechaNotificacion).HasColumnType("datetime");
            entity.Property(e => e.Mensaje).HasMaxLength(300);

            entity.HasOne(d => d.IdEstadoNavigation).WithMany(p => p.Notificaciones)
                .HasForeignKey(d => d.IdEstado)
                .HasConstraintName("FK__Notificac__IdEst__4CA06362");

            entity.HasOne(d => d.IdUsuarioNavigation).WithMany(p => p.Notificaciones)
                .HasForeignKey(d => d.IdUsuario)
                .HasConstraintName("FK__Notificac__IdUsu__4BAC3F29");
        });

        modelBuilder.Entity<Prestamo>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Prestamo__3214EC0768D865CB");

            entity.Property(e => e.FechaDevolucion).HasColumnType("datetime");
            entity.Property(e => e.FechaPrestamo).HasColumnType("datetime");

            entity.HasOne(d => d.IdEstadoNavigation).WithMany(p => p.Prestamos)
                .HasForeignKey(d => d.IdEstado)
                .HasConstraintName("FK__Prestamos__IdEst__44FF419A");

            entity.HasOne(d => d.IdLibroNavigation).WithMany(p => p.Prestamos)
                .HasForeignKey(d => d.IdLibro)
                .HasConstraintName("FK__Prestamos__IdLib__440B1D61");

            entity.HasOne(d => d.IdUsuarioNavigation).WithMany(p => p.Prestamos)
                .HasForeignKey(d => d.IdUsuario)
                .HasConstraintName("FK__Prestamos__IdUsu__4316F928");
        });

        modelBuilder.Entity<Reserva>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Reservas__3214EC07B557C8C5");

            entity.Property(e => e.FechaFinReserva).HasColumnType("datetime");
            entity.Property(e => e.FechaReserva).HasColumnType("datetime");

            entity.HasOne(d => d.IdLibroNavigation).WithMany(p => p.Reservas)
                .HasForeignKey(d => d.IdLibro)
                .HasConstraintName("FK__Reservas__IdLibr__403A8C7D");

            entity.HasOne(d => d.IdUsuarioNavigation).WithMany(p => p.Reservas)
                .HasForeignKey(d => d.IdUsuario)
                .HasConstraintName("FK__Reservas__IdUsua__3F466844");
        });

        modelBuilder.Entity<Usuario>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Usuarios__3214EC07EBAD5215");

            entity.HasIndex(e => e.Email, "UQ__Usuarios__A9D10534F0C80B7B").IsUnique();

            entity.Property(e => e.Contraseña).HasMaxLength(100);
            entity.Property(e => e.Email).HasMaxLength(100);
            entity.Property(e => e.Nombre).HasMaxLength(100);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
