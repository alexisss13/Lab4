namespace Lab4.Models
{
    // Ejercicio 1: Gestión de pedidos en un sistema de delivery
    public class Pedido
    {
        public int Id { get; set; }
        public string Cliente { get; set; } = string.Empty;
        public List<string> Productos { get; set; } = new();
        public string Direccion { get; set; } = string.Empty;
        public string Estado { get; set; } = "Pendiente"; // Pendiente, En camino, Entregado
        public DateTime FechaPedido { get; set; } = DateTime.Now;
    }
}
