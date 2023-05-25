namespace BibliotecaSys.Application.DataObjects;

public class ReservaDto
{
    public int Id { get; set; }

    public int? IdUsuario { get; set; }

    public string? TituloLibro { get; set; }

    public DateTime FechaReserva { get; set; }

    public DateTime FechaFinReserva { get; set; }
}