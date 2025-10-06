using BiblioAwoo.Infrastructure;
using BiblioAwoo.Models;
using Microsoft.AspNetCore.Mvc;

namespace BiblioAwoo.Controllers;

public class UsuarioController : Controller
{
    private readonly AppDbContext _context;
    
    public UsuarioController(AppDbContext context)
    {
        _context = context;
    }
    
    public IActionResult Index()
    {
        var usuarios = _context.Usuarios.ToList();
        ViewBag.Libros = _context.Libros.ToList();
        return View(usuarios);
    }

    public IActionResult Create()
    {
        return View();
    }
    
    [HttpPost]
    public IActionResult Create(Usuario usuario)
    {
        try
        {
            var existe = _context.Usuarios.Any(u => u.Documento == usuario.Documento);
            if (existe)
            {
                ViewBag.Error = "Ya existe un usuario con ese documento.";
                var usuarios = _context.Usuarios.ToList();
                ViewBag.Libros = _context.Libros.ToList();
                return View("Index", usuarios);
            }

            _context.Usuarios.Add(usuario);
            _context.SaveChanges();

            return RedirectToAction("Index");
        }
        catch (Exception err)
        {
            ViewBag.Error = $"Error al registrar usuario: {err.Message}";
            var usuarios = _context.Usuarios.ToList();
            ViewBag.Libros = _context.Libros.ToList();
            return View("Index", usuarios);
        }
    }

    
    public IActionResult Details(int id)
    {
        var usuario = _context.Usuarios.FirstOrDefault(u => u.Id == id);

        if (usuario == null)
        {
            ViewBag.Error = "No se encontró el usuario.";
            return View();
        }

        return View(usuario);
    }
}
    

