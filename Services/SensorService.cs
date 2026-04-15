using Lab4.Models;
using System.Text.Json;

namespace Lab4.Services
{
    public class SensorService
    {
        private readonly string _dataFolder;
        private const long MAX_FILE_SIZE = 1024 * 100; // 100 KB para rotación
        private static readonly SemaphoreSlim _semaphore = new(1, 1);

        public SensorService(IWebHostEnvironment env)
        {
            _dataFolder = Path.Combine(env.ContentRootPath, "Data", "Sensores");
            if (!Directory.Exists(_dataFolder))
                Directory.CreateDirectory(_dataFolder);
        }

        public async Task GuardarLecturaAsync(LecturaSensor lectura)
        {
            await _semaphore.WaitAsync();
            try
            {
                var archivoActual = ObtenerArchivoActual();
                var lecturas = await LeerArchivo(archivoActual);
                
                lectura.Id = lecturas.Any() ? lecturas.Max(l => l.Id) + 1 : 1;
                lecturas.Add(lectura);

                await EscribirArchivo(archivoActual, lecturas);

                // Verificar si necesita rotación
                var fileInfo = new FileInfo(archivoActual);
                if (fileInfo.Length > MAX_FILE_SIZE)
                {
                    await RotarArchivo();
                }
            }
            finally
            {
                _semaphore.Release();
            }
        }

        public async Task<List<LecturaSensor>> ObtenerTodasLecturasAsync()
        {
            await _semaphore.WaitAsync();
            try
            {
                var todasLecturas = new List<LecturaSensor>();
                var archivos = Directory.GetFiles(_dataFolder, "sensores_*.json")
                    .OrderBy(f => f).ToList();

                foreach (var archivo in archivos)
                {
                    var lecturas = await LeerArchivo(archivo);
                    todasLecturas.AddRange(lecturas);
                }

                return todasLecturas;
            }
            finally
            {
                _semaphore.Release();
            }
        }

        public async Task<Dictionary<string, List<LecturaSensor>>> ObtenerLecturasPorSensorAsync()
        {
            var lecturas = await ObtenerTodasLecturasAsync();
            return lecturas.GroupBy(l => l.SensorId)
                .ToDictionary(g => g.Key, g => g.OrderBy(l => l.FechaLectura).ToList());
        }

        private string ObtenerArchivoActual()
        {
            var archivos = Directory.GetFiles(_dataFolder, "sensores_*.json");
            if (archivos.Length == 0)
            {
                return Path.Combine(_dataFolder, $"sensores_{DateTime.Now:yyyyMMdd_HHmmss}.json");
            }
            return archivos.OrderByDescending(f => f).First();
        }

        private async Task RotarArchivo()
        {
            // El siguiente archivo se creará automáticamente con nuevo timestamp
            var nuevoArchivo = Path.Combine(_dataFolder, $"sensores_{DateTime.Now:yyyyMMdd_HHmmss}.json");
            await EscribirArchivo(nuevoArchivo, new List<LecturaSensor>());
        }

        private async Task<List<LecturaSensor>> LeerArchivo(string filePath)
        {
            if (!File.Exists(filePath))
                return new List<LecturaSensor>();

            var json = await File.ReadAllTextAsync(filePath);
            return JsonSerializer.Deserialize<List<LecturaSensor>>(json) ?? new List<LecturaSensor>();
        }

        private async Task EscribirArchivo(string filePath, List<LecturaSensor> lecturas)
        {
            var options = new JsonSerializerOptions { WriteIndented = true };
            var json = JsonSerializer.Serialize(lecturas, options);
            await File.WriteAllTextAsync(filePath, json);
        }
    }
}
