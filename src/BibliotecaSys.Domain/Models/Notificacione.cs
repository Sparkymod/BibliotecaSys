using System;
using System.Collections.Generic;

namespace BibliotecaSys.Domain.Models;

public partial class Notificacione
{
    public int Id { get; set; }

    public int? IdUsuario { get; set; }

    public DateTime FechaNotificacion { get; set; }

    public string Mensaje { get; set; } = null!;

    public int? IdEstado { get; set; }

    public virtual Estado? IdEstadoNavigation { get; set; }

    public virtual Usuario? IdUsuarioNavigation { get; set; }
}
