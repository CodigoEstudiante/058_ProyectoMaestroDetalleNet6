namespace WebApplication1.Models
{
    public class Venta
    {
        public int IdVenta { get; set; }
        public string NumeroDocumento { get; set; }
        public string RazonSocial { get; set; }
        public decimal Total { get; set; }
        public List<Detalle_Venta> lstDetalleVenta { get; set; }
    }
}
