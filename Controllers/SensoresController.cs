using Lab4.Models;
using Lab4.Services;
using Microsoft.AspNetCore.Mvc;

namespace Lab4.Controllers
{
    public class SensoresController : Controller
    {
        private readonly SensorService _sensorService;

        public SensoresController(SensorService sensorService)
        {
            _sensorService = sensorService;
        }

        public async Task<IActionResult> Index()
        {
            var lecturas = await _sensorService.ObtenerTodasLecturasAsync();
            return View(lecturas.OrderByDescending(l => l.FechaLectura).Take(50).ToList());
        }

        public IActionResult Registrar()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Registrar(LecturaSensor lectura)
        {
            await _sensorService.GuardarLecturaAsync(lectura);
            TempData["Mensaje"] = "Lectura registrada exitosamente";
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Graficas()
        {
            var lecturasPorSensor = await _sensorService.ObtenerLecturasPorSensorAsync();
            return View(lecturasPorSensor);
        }

        [HttpPost]
        public async Task<IActionResult> SimularLecturas(int cantidad = 10)
        {
            var random = new Random();
            var sensores = new[] { "SENSOR-001", "SENSOR-002", "SENSOR-003" };
            var ubicaciones = new[] { "Sala A", "Sala B", "Exterior" };

            for (int i = 0; i < cantidad; i++)
            {
                var lectura = new LecturaSensor
                {
                    SensorId = sensores[random.Next(sensores.Length)],
                    Temperatura = Math.Round(15 + random.NextDouble() * 20, 2),
                    Humedad = Math.Round(30 + random.NextDouble() * 50, 2),
                    Ubicacion = ubicaciones[random.Next(ubicaciones.Length)],
                    FechaLectura = DateTime.Now.AddMinutes(-i)
                };
                await _sensorService.GuardarLecturaAsync(lectura);
            }

            TempData["Mensaje"] = $"{cantidad} lecturas simuladas exitosamente";
            return RedirectToAction(nameof(Index));
        }
    }
}
