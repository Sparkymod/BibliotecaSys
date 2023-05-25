namespace BibliotecaSys.Application.DataObjects;

public class PrestamoDto
{
    public int Id { get; set; }

    public int? IdUsuario { get; set; }

    public int? IdLibro { get; set; }

    public DateTime FechaPrestamo { get; set; }

    public DateTime? FechaDevolucion { get; set; }

    public int? IdEstado { get; set; }
}