using Lab4.Models;
using System.Text.Json;

namespace Lab4.Services
{
    public class PedidoService
    {
        private readonly string _filePath;
        private static readonly SemaphoreSlim _semaphore = new(1, 1);

        public PedidoService(IWebHostEnvironment env)
        {
            var dataFolder = Path.Combine(env.ContentRootPath, "Data");
            if (!Directory.Exists(dataFolder))
                Directory.CreateDirectory(dataFolder);
            
            _filePath = Path.Combine(dataFolder, "pedidos.json");
        }

        public async Task<List<Pedido>> ObtenerTodosAsync()
        {
            await _semaphore.WaitAsync();
            try
            {
                if (!File.Exists(_filePath))
                    return new List<Pedido>();

                var json = await File.ReadAllTextAsync(_filePath);
                return JsonSerializer.Deserialize<List<Pedido>>(json) ?? new List<Pedido>();
            }
            finally
            {
                _semaphore.Release();
            }
        }

        public async Task GuardarPedidoAsync(Pedido pedido)
        {
            await _semaphore.WaitAsync();
            try
            {
                var pedidos = await ObtenerTodosInternalAsync();
                pedido.Id = pedidos.Any() ? pedidos.Max(p => p.Id) + 1 : 1;
                pedidos.Add(pedido);

                var options = new JsonSerializerOptions { WriteIndented = true };
                var json = JsonSerializer.Serialize(pedidos, options);
                await File.WriteAllTextAsync(_filePath, json);
            }
            finally
            {
                _semaphore.Release();
            }
        }

        public async Task ActualizarEstadoAsync(int id, string nuevoEstado)
        {
            await _semaphore.WaitAsync();
            try
            {
                var pedidos = await ObtenerTodosInternalAsync();
                var pedido = pedidos.FirstOrDefault(p => p.Id == id);
                if (pedido != null)
                {
                    pedido.Estado = nuevoEstado;
                    var options = new JsonSerializerOptions { WriteIndented = true };
                    var json = JsonSerializer.Serialize(pedidos, options);
                    await File.WriteAllTextAsync(_filePath, json);
                }
            }
            finally
            {
                _semaphore.Release();
            }
        }

        private async Task<List<Pedido>> ObtenerTodosInternalAsync()
        {
            if (!File.Exists(_filePath))
                return new List<Pedido>();

            var json = await File.ReadAllTextAsync(_filePath);
            return JsonSerializer.Deserialize<List<Pedido>>(json) ?? new List<Pedido>();
        }
    }
}
