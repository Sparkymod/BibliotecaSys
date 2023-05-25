namespace BibliotecaSys.Application.DataObjects;

public class NotificacioneDto
{
    public int Id { get; set; }

    public int? IdUsuario { get; set; }

    public DateTime FechaNotificacion { get; set; }

    public string Mensaje { get; set; } = null!;

    public int? IdEstado { get; set; }
}