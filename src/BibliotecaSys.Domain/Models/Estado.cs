using System;
using System.Collections.Generic;

namespace BibliotecaSys.Domain.Models;

public partial class Estado
{
    public int Id { get; set; }

    public string Estado1 { get; set; } = null!;

    public virtual ICollection<Libro> Libros { get; set; } = new List<Libro>();

    public virtual ICollection<Notificacione> Notificaciones { get; set; } = new List<Notificacione>();

    public virtual ICollection<Prestamo> Prestamos { get; set; } = new List<Prestamo>();
}
