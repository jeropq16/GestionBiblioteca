using BiblioAwoo.Infrastructure;
using BiblioAwoo.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BiblioAwoo.Controllers;

public class PrestamoController : Controller 
{
    private readonly AppDbContext _context;

    public PrestamoController(AppDbContext context)
    {
        _context = context;
    }
    
    public ActionResult Index()
    {
        var prestamos = _context.Prestamos
            .Include(p => p.Usuario)
            .Include(p => p.Libro)
            .ToList();

        ViewBag.Usuarios = _context.Usuarios.ToList();
        ViewBag.Libros = _context.Libros.ToList();

        return View(prestamos);
    }

    [HttpPost]
    public IActionResult Crear(Prestamo prestamo)
    {
        try
        {
            var libro = _context.Libros.FirstOrDefault(l => l.Id == prestamo.LibroId);
            if (libro == null)
            {
                ViewBag.Error = "El libro seleccionado no existe.";
            }
            else if (libro.EjemplaresDisponibles <= 0)
            {
                ViewBag.Error = "No hay ejemplares disponibles para este libro.";
            }
            else
            {
                libro.EjemplaresDisponibles -= 1;
                _context.Prestamos.Add(prestamo);
                _context.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.Libros = _context.Libros.ToList();
            ViewBag.Usuarios = _context.Usuarios.ToList();
            var prestamos = _context.Prestamos.ToList();
            return View("Index", prestamos);
        }
        catch (Exception err)
        {
            ViewBag.Error = $"Error al registrar préstamo: {err.Message}";
            ViewBag.Libros = _context.Libros.ToList();
            ViewBag.Usuarios = _context.Usuarios.ToList();
            var prestamos = _context.Prestamos.ToList();
            return View("Index", prestamos);
        }
    }


    public IActionResult Devolver(int id)
    {
        var prestamo = _context.Prestamos
            .Include(p => p.Libro)
            .FirstOrDefault(p => p.Id == id);

        if (prestamo != null)
        {
            prestamo.Libro.EjemplaresDisponibles += 1;
            _context.Prestamos.Remove(prestamo);
            _context.SaveChanges();
        }

        return RedirectToAction("Index");
    }

    public IActionResult HistorialUsuario(int id)
    {
        var prestamos = _context.Prestamos
            .Include(p => p.Libro)
            .Where(p => p.UsuarioId == id)
            .ToList();

        ViewBag.Usuario = _context.Usuarios.Find(id);
        return View("HistorialUsuario", prestamos);
    }
    
    public IActionResult PrestamosPorLibro(int id)
    {
        var prestamos = _context.Prestamos
            .Include(p => p.Usuario)
            .Where(p => p.LibroId == id)
            .ToList();

        ViewBag.Libro = _context.Libros.Find(id);
        return View("PrestamosPorLibro", prestamos);
    }

}