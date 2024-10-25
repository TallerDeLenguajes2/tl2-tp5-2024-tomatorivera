namespace Models;

public class Presupuesto
{
    private static float IVA = 0.21f;

    private int idPresupuesto;
    private string nombreDestinatario;
    private List<PresupuestoDetalle> detalle;

    public Presupuesto(int idPresupuesto, string nombreDestinatario, List<PresupuestoDetalle> detalle)
    {
        this.idPresupuesto = idPresupuesto;
        this.nombreDestinatario = nombreDestinatario;
        this.detalle = detalle;
    }

    public int IdPresupuesto { get => idPresupuesto; set => idPresupuesto = value; }
    public string NombreDestinatario { get => nombreDestinatario; set => nombreDestinatario = value; }
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