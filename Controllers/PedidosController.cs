using Lab4.Models;
using Lab4.Services;
using Microsoft.AspNetCore.Mvc;

namespace Lab4.Controllers
{
    public class PedidosController : Controller
    {
        private readonly PedidoService _pedidoService;

        public PedidosController(PedidoService pedidoService)
        {
            _pedidoService = pedidoService;
        }

        public async Task<IActionResult> Index()
        {
            var pedidos = await _pedidoService.ObtenerTodosAsync();
            return View(pedidos);
        }

        public IActionResult Crear()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Crear(Pedido pedido, string productosTexto)
        {
            if (!string.IsNullOrWhiteSpace(productosTexto))
            {
                pedido.Productos = productosTexto.Split(',')
                    .Select(p => p.Trim())
                    .Where(p => !string.IsNullOrEmpty(p))
                    .ToList();
            }

            await _pedidoService.GuardarPedidoAsync(pedido);
            TempData["Mensaje"] = "Pedido creado exitosamente";
            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        public async Task<IActionResult> ActualizarEstado(int id, string estado)
        {
            await _pedidoService.ActualizarEstadoAsync(id, estado);
            return RedirectToAction(nameof(Index));
        }
    }
}
