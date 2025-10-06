using BiblioAwoo.Infrastructure;
using BiblioAwoo.Models;
using Microsoft.AspNetCore.Mvc;

namespace BiblioAwoo.Controllers;

public class LibroController : Controller
{
    private readonly AppDbContext _context;

    public LibroController(AppDbContext context)
    {
        _context = context;
    }

    public IActionResult Index()
    {
        var libros = _context.Libros.ToList();
        return View(libros);
    }

    [HttpPost]
    public IActionResult Crear(Libro libro)
    {
        try
        {
            var existe = _context.Libros.Any(l => l.Codigo == libro.Codigo);
            if (existe)
            {
                ViewBag.Error = "Ya existe un libro con ese código.";
                var libros = _context.Libros.ToList();
                return View("Index", libros);
            }

            _context.Libros.Add(libro);
            _context.SaveChanges();

            return RedirectToAction("Index");
        }
        catch (Exception err)
        {
            ViewBag.Error = $"Error al registrar libro: {err.Message}";
            var libros = _context.Libros.ToList();
            return View("Index", libros);
        }
    }

    
    public IActionResult Details()
    {
        return View();
    }
}
