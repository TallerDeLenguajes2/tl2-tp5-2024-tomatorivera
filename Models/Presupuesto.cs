namespace Models;

public class Presupuesto
{
    private static float IVA = 0.21f;

    private int idPresupuesto;
    private string nombreDestinatario;
    private string fechaCreacion;
    private List<PresupuestoDetalle> detalle;

    public Presupuesto()
    {
        idPresupuesto = -1;
        nombreDestinatario = string.Empty;
        fechaCreacion = string.Empty;
        detalle = new List<PresupuestoDetalle>();
    }

    public Presupuesto(int idPresupuesto, string nombreDestinatario, string fechaCreacion)
    {
        this.idPresupuesto = idPresupuesto;
        this.nombreDestinatario = nombreDestinatario;
        this.fechaCreacion = fechaCreacion;
        this.detalle = new List<PresupuestoDetalle>();
    }

    public int IdPresupuesto { get => idPresupuesto; set => idPresupuesto = value; }
    public string NombreDestinatario { get => nombreDestinatario; set => nombreDestinatario = value; }
    public string FechaCreacion { get => fechaCreacion; set => fechaCreacion = value; }
    public List<PresupuestoDetalle> Detalle { get => detalle; set => detalle = value; }

    public float MontoPresupuesto()
    {
        return detalle.Select(d => d.Producto.Precio * d.Cantidad).Sum();
    }

    public float MontoPresupuestoConIva()
    {
        return MontoPresupuesto() * (1 + IVA);
    }

    public int CantidadProductos()
    {
        return detalle.Select(d => d.Cantidad).Sum();
    }
}