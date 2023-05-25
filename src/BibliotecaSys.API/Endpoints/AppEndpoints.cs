
using AutoMapper;
using BibliotecaSys.Application.Common;
using BibliotecaSys.Application.DataObjects;
using BibliotecaSys.Domain.Models;
using BibliotecaSys.Infrastructure.Repositories;
using DocumentFormat.OpenXml.Office2010.Excel;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BibliotecaSys.API.Controllers;

public static class AppEndpoints
{
    public static void MapLibrosEndpoints(this WebApplication app)
    {
        // GET
        app.MapGet("/libros", GetAllWithFilters);
        app.MapGet("/libros/{id}", GetById);
        app.MapGet("/usuarios/{id}/historial", GetAllHistorialPrestamo);

        // POST
        app.MapPost("/libros/{id}/reserva", ReservarLibro);

        // PUT
        app.MapPut("/reservas/{id}/renovar", RenovarLibro);
    }

    [SwaggerDescription("Contiene filtros como titulo, autor y genero.")]
    private static async Task<IResult> GetAllWithFilters(IRepository<Libro> repository, IMapper mapper, string? titulo, string? autor, string? genero)
    {
        var libros = await repository.GetAllAsync();

        if (!string.IsNullOrWhiteSpace(titulo))
        {
            libros = libros.Where(l => l.Titulo.Contains(titulo));
        }

        if (!string.IsNullOrWhiteSpace(autor))
        {
            libros = libros.Where(l => l.Autor.Contains(autor));
        }

        if (!string.IsNullOrWhiteSpace(genero))
        {
            libros = libros.Where(l => l.Genero.Contains(genero));
        }

        var resultDto = mapper.Map<List<LibroDto>>(libros.ToList());

        return resultDto.Any() ? Results.Ok(resultDto) : Results.NotFound();
    }

    [SwaggerDescription("Obtiene un libro por su Id.")]
    private static async Task<IResult> GetById(IRepository<Libro> repository, int id, IMapper mapper)
    {
        var libro = await repository.GetByIdAsync(id);
        var resultDto = mapper.Map<LibroDto>(libro);

        return libro is not null ? Results.Ok(Results.Json(resultDto)) : Results.NotFound();
    }

    [SwaggerDescription("Reservar el libro por id y respuesta del cliente.")]
    private static async Task<IResult> ReservarLibro(IRepository<Libro> repositoryLibro, 
        IRepository<Reserva> repositoryReserva, int id, [FromBody] ReservaDto reservaDto)
    {
        var libro = await repositoryLibro.GetByIdAsync(id);

        if (libro == null)
        {
            return Results.NotFound("Libro no encontrado.");
        }

        // Este estado puede vivir en una clase que sea Constantes y ponerlo ahi.
        if (libro.IdEstadoNavigation.Estado1 is "Reservado")
        {
            return Results.BadRequest("El libro ya ha sido reservado.");
        }

        var reserva = new Reserva
        {
            IdUsuario = reservaDto.IdUsuario,
            FechaReserva = reservaDto.FechaReserva,
            FechaFinReserva = reservaDto.FechaFinReserva
        };

        libro.IdEstado = 2;
        await repositoryLibro.UpdateAsync(libro);
        await repositoryReserva.CreateAsync(reserva);

        return Results.Ok("Libro reservado con éxito.");
    }
    
    [SwaggerDescription("Renovar el libro con el id de la reserva.")]
    private static async Task<IResult> RenovarLibro(IRepository<Libro> repositoryLibro,
        IRepository<Reserva> repositoryReserva, int id)
    {
        var reserva = await repositoryReserva.GetByIdAsync(id);

        if (reserva is null)
        {
            return Results.NotFound("Reserva no encontrada.");
        }

        var libro = await repositoryLibro.GetByIdAsync(reserva.IdLibro ?? 0);

        if (libro is null)
        {
            return Results.NotFound("Libro de la reserva no encontrado.");
        }

        if (libro.IdEstadoNavigation.Estado1 is "Reservado")
        {
            return Results.Problem("El libro ya ha sido reservado por otro usuario.");
        }

        reserva.FechaFinReserva = reserva.FechaFinReserva.AddDays(30);

        await repositoryReserva.UpdateAsync(reserva);

        return Results.Ok("Préstamo renovado con éxito.");
    }

    [SwaggerDescription("Obtener un historial de préstamos, a travez del id del libro.")]
    private static async Task<IResult> GetAllHistorialPrestamo(IRepository<Reserva> repository, int id, IMapper mapper)
    {
        var reservas = await repository.AsQueryable()
            .Where(r => r.IdLibro == id)
            .Include(r => r.IdLibro)  // Para obtener detalles del libro
            .ToListAsync();

        if (!reservas.Any())
        {
            return Results.NotFound("No se encontró historial de préstamos para el usuario.");
        }

        var reservasDto = mapper.Map<List<ReservaDto>>(reservas);

        return Results.Ok(reservasDto);
    }
}
