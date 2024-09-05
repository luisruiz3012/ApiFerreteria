namespace VentasLibrary.Models
{
    public class Venta
    {
        public int Id { get; set; }
        public int Producto_Id { get; set; }
        public int Empleado_Id { get; set; }
        public int Unidades { get; set;}
        public decimal Total { get; set;}
        public string Created_At { get; set; }

    }
}
