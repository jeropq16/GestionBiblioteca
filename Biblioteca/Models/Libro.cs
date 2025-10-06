namespace BiblioAwoo.Models;

public class Libro
{
    public int Id { get; set; }
    public string Titulo { get; set; }
    public string Autor { get; set; }
    public string Codigo { get; set; }
    public int EjemplaresDisponibles { get; set; }
    public List<Prestamo> Prestamos { get; set; } = new List<Prestamo>();
}