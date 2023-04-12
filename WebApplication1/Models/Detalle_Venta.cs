namespace WebApplication1.Models
{
    public class Detalle_Venta
    {
        public int IdDetalleVenta { get; set; }
        public string Producto { get; set; }
        public decimal Precio { get; set; }
        public int Cantidad { get; set; }
        public decimal Total { get; set; }
    }
}
