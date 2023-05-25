using System;
using System.Collections.Generic;

namespace BibliotecaSys.Domain.Models;

public partial class Prestamo
{
    public int Id { get; set; }

    public int? IdUsuario { get; set; }

    public int? IdLibro { get; set; }

    public DateTime FechaPrestamo { get; set; }

    public DateTime? FechaDevolucion { get; set; }

    public int? IdEstado { get; set; }

    public virtual Estado? IdEstadoNavigation { get; set; }

    public virtual Libro? IdLibroNavigation { get; set; }

    public virtual Usuario? IdUsuarioNavigation { get; set; }
}
