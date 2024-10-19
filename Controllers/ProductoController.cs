using Microsoft.AspNetCore.Mvc;
using Models;
using Persistence;

namespace Controllers;

[ApiController]
[Route("[controller]")]
public class ProductoController : ControllerBase
{
    private readonly ProductoRepository repo;

    public ProductoController() {
        repo = new ProductoRepository();
    }

    [HttpGet("api/ObtenerProductos")]
    public ActionResult<List<Producto>> GetProductos() {
        try {
            return Ok(repo.Listar());
        } catch (Exception ex) {
            return StatusCode(500, $"ERROR: {ex.Message}");
        }
    }

    [HttpPost("api/AgregarProducto")]
    public ActionResult AgregarProducto(string descripcion, int precio)
    {
        try {
            var nuevoProducto = new Producto(descripcion, precio);
            repo.Insertar(nuevoProducto);
            return Ok($"Se ha agregado el nuevo producto: {descripcion} - ${precio}");
        } catch (Exception ex) {
            return StatusCode(500, $"ERROR: {ex.Message}");
        }
    }

    [HttpPut("api/ModificarProducto/{id}")]
    public ActionResult ModificarProducto(int id, string descripcion)
    {
        try {
            var producto = repo.Obtener(id);
            producto.Descripcion = descripcion;
            repo.Modificar(id, producto);
            return Ok($"Producto modificado: {descripcion}");
        } catch (Exception ex) {
            return StatusCode(500, $"ERROR: {ex.Message}");
        }
    }
}