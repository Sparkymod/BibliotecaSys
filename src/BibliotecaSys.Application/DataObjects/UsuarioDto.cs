﻿namespace BibliotecaSys.Application.DataObjects;

public class UsuarioDto
{
    public int Id { get; set; }

    public string Nombre { get; set; } = null!;

    public string Email { get; set; } = null!;
}