using System;
using System.Collections.Generic;

namespace BibliotecaSys.Domain.Models;

public partial class Libro
{
    public int Id { get; set; }

    public string Titulo { get; set; } = null!;

    public string Autor { get; set; } = null!;

    public string Genero { get; set; } = null!;

    public int IdEstado { get; set; }

    public virtual ICollection<HistorialPrestamo> HistorialPrestamos { get; set; } = new List<HistorialPrestamo>();

    public virtual Estado IdEstadoNavigation { get; set; } = null!;

    public virtual ICollection<Prestamo> Prestamos { get; set; } = new List<Prestamo>();

    public virtual ICollection<Reserva> Reservas { get; set; } = new List<Reserva>();
}
