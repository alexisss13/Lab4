namespace Lab4.Models
{
    // Ejercicio 3: Registro de sensores IoT para monitoreo ambiental
    public class LecturaSensor
    {
        public int Id { get; set; }
        public string SensorId { get; set; } = string.Empty;
        public double Temperatura { get; set; }
        public double Humedad { get; set; }
        public DateTime FechaLectura { get; set; } = DateTime.Now;
        public string Ubicacion { get; set; } = string.Empty;
    }
}
