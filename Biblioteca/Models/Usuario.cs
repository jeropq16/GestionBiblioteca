namespace BiblioAwoo.Models;

public class Usuario
{
    public int Id { get; set; }
    public string Nombre { get; set; }
    public string Documento { get; set; }
    public string Correo { get; set; }
    public string Telefono { get; set; }
    public List<Prestamo> Prestamos { get; set; } = new List<Prestamo>();
}