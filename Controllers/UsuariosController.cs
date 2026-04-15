using Lab4.Models;
using Lab4.Services;
using Microsoft.AspNetCore.Mvc;

namespace Lab4.Controllers
{
    public class UsuariosController : Controller
    {
        private readonly UsuarioService _usuarioService;

        public UsuariosController(UsuarioService usuarioService)
        {
            _usuarioService = usuarioService;
        }

        public async Task<IActionResult> Index()
        {
            var usuarios = await _usuarioService.ObtenerTodosAsync();
            return View(usuarios);
        }

        public IActionResult Crear()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Crear(Usuario usuario)
        {
            await _usuarioService.GuardarUsuarioAsync(usuario);
            TempData["Mensaje"] = "Usuario registrado exitosamente";
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Editar(int id)
        {
            var usuarios = await _usuarioService.ObtenerTodosAsync();
            var usuario = usuarios.FirstOrDefault(u => u.Id == id);
            if (usuario == null)
                return NotFound();
            
            return View(usuario);
        }

        [HttpPost]
        public async Task<IActionResult> Editar(Usuario usuario)
        {
            await _usuarioService.ActualizarUsuarioAsync(usuario);
            TempData["Mensaje"] = "Usuario actualizado exitosamente";
            return RedirectToAction(nameof(Index));
        }
    }
}
