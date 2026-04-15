using Lab4.Models;

namespace Lab4.Services
{
    public class UsuarioService
    {
        private readonly string _filePath;
        private static readonly SemaphoreSlim _semaphore = new(1, 1);

        public UsuarioService(IWebHostEnvironment env)
        {
            var dataFolder = Path.Combine(env.ContentRootPath, "Data");
            if (!Directory.Exists(dataFolder))
                Directory.CreateDirectory(dataFolder);
            
            _filePath = Path.Combine(dataFolder, "usuarios.json");
        }

        public async Task<List<Usuario>> ObtenerTodosAsync()
        {
            await _semaphore.WaitAsync();
            try
            {
                return await LeerArchivo();
            }
            finally
            {
                _semaphore.Release();
            }
        }

        public async Task GuardarUsuarioAsync(Usuario usuario)
        {
            await _semaphore.WaitAsync();
            try
            {
                var usuarios = await LeerArchivo();
                usuario.Id = usuarios.Any() ? usuarios.Max(u => u.Id) + 1 : 1;
                usuarios.Add(usuario);
                await EscribirArchivo(usuarios);
            }
            finally
            {
                _semaphore.Release();
            }
        }

        public async Task ActualizarUsuarioAsync(Usuario usuario)
        {
            await _semaphore.WaitAsync();
            try
            {
                var usuarios = await LeerArchivo();
                var index = usuarios.FindIndex(u => u.Id == usuario.Id);
                if (index >= 0)
                {
                    usuarios[index] = usuario;
                    await EscribirArchivo(usuarios);
                }
            }
            finally
            {
                _semaphore.Release();
            }
        }

        // Uso de StreamReader para leer el archivo JSON
        private async Task<List<Usuario>> LeerArchivo()
        {
            if (!File.Exists(_filePath))
                return new List<Usuario>();

            using var reader = new StreamReader(_filePath);
            var json = await reader.ReadToEndAsync();
            return System.Text.Json.JsonSerializer.Deserialize<List<Usuario>>(json) ?? new List<Usuario>();
        }

        // Uso de StreamWriter para escribir el archivo JSON
        private async Task EscribirArchivo(List<Usuario> usuarios)
        {
            var options = new System.Text.Json.JsonSerializerOptions { WriteIndented = true };
            var json = System.Text.Json.JsonSerializer.Serialize(usuarios, options);
            
            using var writer = new StreamWriter(_filePath, false);
            await writer.WriteAsync(json);
        }
    }
}
