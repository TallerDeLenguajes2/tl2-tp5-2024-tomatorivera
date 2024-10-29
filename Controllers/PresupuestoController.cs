using Microsoft.AspNetCore.Mvc;
using Models;
using Persistence;

namespace Controllers;

[ApiController]
[Route("[controller]")]
public class PresupuestoController: ControllerBase
{

    private readonly IPresupuestoRepository repoPresupuestos;

    public PresupuestoController() {
        repoPresupuestos = new PresupuestoRepository(); 
    }

    [HttpGet("api/presupuesto")]
    public ActionResult<List<Presupuesto>> GetPresupuestos() {
        try {
            return Ok(repoPresupuestos.Listar());
        } catch (Exception ex) {
            return StatusCode(500, $"ERROR: {ex.Message}");
        }
    }

    [HttpGet("api/presupuesto/{id}")]
    public ActionResult<Presupuesto> GetPresupuesto(int id) {
        try {
            var presupuesto = repoPresupuestos.Obtener(id);
            if (presupuesto.Id == -1)
                throw new Exception($"No se ha encontrado un presupuesto de id {id}");

            return Ok(presupuesto);
        } catch (Exception ex) {
            return StatusCode(500, $"ERROR: {ex.Message}");
        }
    }

    [HttpPost("api/AgregarPresupuesto")]
    public ActionResult AgregarPresupuesto(string nombreDestinatario) {
        try {
            var presupuesto = new Presupuesto(nombreDestinatario, DateTime.Now.ToString("yyyy-MM-dd"));
            repoPresupuestos.Insertar(presupuesto);
            return Ok("Presupuesto insertado con éxito");
        } catch (Exception ex) {
            return StatusCode(500, $"ERROR: {ex.Message}");
        }
    }

    [HttpPost("api/presupuesto/{idPresupuesto}/ProductoDetalle")]
    public ActionResult AgregarProductoDetalle(int idPresupuesto, int idProducto, int cantidad) {
        try {
            repoPresupuestos.InsertarDetalle(idPresupuesto, idProducto, cantidad);
            return Ok("Producto agregado al detalle con éxito");
        } catch (Exception ex) {
            return StatusCode(500, $"ERROR: {ex.Message}");
        }
    }

}