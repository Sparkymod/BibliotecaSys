using System;
using System.Collections.Generic;

namespace BibliotecaSys.Domain.Models;

public partial class Usuario
{
    public int Id { get; set; }

    public string Nombre { get; set; } = null!;

    public string Email { get; set; } = null!;

    public string Contraseña { get; set; } = null!;

    public virtual ICollection<HistorialPrestamo> HistorialPrestamos { get; set; } = new List<HistorialPrestamo>();

    public virtual ICollection<Notificacione> Notificaciones { get; set; } = new List<Notificacione>();

    public virtual ICollection<Prestamo> Prestamos { get; set; } = new List<Prestamo>();

    public virtual ICollection<Reserva> Reservas { get; set; } = new List<Reserva>();
}
