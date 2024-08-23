using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PedidosApi.Data;
using PedidosApi.Models;

namespace PedidosApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PedidoController : ControllerBase
    {
        private readonly AppDbContext _context;

        public PedidoController(AppDbContext context)
        {
            _context = context;
        }

        // GET: api/Pedido
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Pedido>>> GetPedidos()
        {
            return await _context.Pedidos.ToListAsync();
        }


        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetPedido(int id)
        {
            var pedido = await _context.Pedidos
                .Include(p => p.ItensPedido)
                    .ThenInclude(ip => ip.Produto)
                .FirstOrDefaultAsync(p => p.Id == id);

            if (pedido == null)
            {
                return NotFound();
            }

            var result = new
            {
                pedido.Id,
                pedido.NomeCliente,
                pedido.EmailCliente,
                pedido.Pago,
                ValorTotal = pedido.ItensPedido.Sum(ip => ip.Produto.Valor * ip.Quantidade),
                ItensPedido = pedido.ItensPedido.Select(ip => new
                {
                    ip.Id,
                    ip.IdProduto,
                    ip.Produto.NomeProduto,
                    ValorUnitario = ip.Produto.Valor,
                    ip.Quantidade
                }).ToList()
            };

            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> CreateOrUpdatePedido([FromBody] CreatePedidoDto createPedidoDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            Pedido pedido;
            if (createPedidoDto.Id.HasValue) // Verifica se o ID foi passado para atualizar
            {
                pedido = await _context.Pedidos
                                               .Include(p => p.ItensPedido)
                                               .FirstOrDefaultAsync(p => p.Id == createPedidoDto.Id.Value);
                if (pedido == null)
                {
                    return NotFound($"Pedido com Id {createPedidoDto.Id} não encontrado.");
                }

                // Atualiza os campos do pedido
                pedido.NomeCliente = createPedidoDto.NomeCliente;
                pedido.EmailCliente = createPedidoDto.EmailCliente;
                pedido.Pago = createPedidoDto.Pago;

                // Remove itens existentes para substituí-los pelos novos itens
                _context.ItensPedido.RemoveRange(pedido.ItensPedido);
            }
            else
            {
                pedido = new Pedido
                {
                    NomeCliente = createPedidoDto.NomeCliente,
                    EmailCliente = createPedidoDto.EmailCliente,
                    DataCriacao = DateTime.UtcNow,
                    Pago = createPedidoDto.Pago
                };

                _context.Pedidos.Add(pedido);
                await _context.SaveChangesAsync(); // Salva o pedido para obter o ID
            }

            // Adiciona itens do pedido
            foreach (var itemDto in createPedidoDto.Itens)
            {
                var produto = await _context.Produtos.FindAsync(itemDto.IdProduto);
                if (produto == null)
                {
                    return BadRequest($"Produto com Id {itemDto.IdProduto} não encontrado.");
                }

                var item = new ItensPedido
                {
                    IdPedido = pedido.Id,
                    IdProduto = itemDto.IdProduto,
                    Quantidade = itemDto.Quantidade,
                    Produto = produto // Associando o produto ao item
                };
                _context.ItensPedido.Add(item);
            }

            await _context.SaveChangesAsync();

            return createPedidoDto.Id.HasValue
                ? Ok(pedido) // Retorna 200 OK se foi uma atualização
                : CreatedAtAction(nameof(GetPedido), new { id = pedido.Id }, pedido); // Retorna 201 Created se foi uma criação
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> DeletePedido(int id)
        {
            var pedido = await _context.Pedidos
                .Include(p => p.ItensPedido)
                .FirstOrDefaultAsync(p => p.Id == id);

            if (pedido == null)
            {
                return NotFound();
            }

            _context.ItensPedido.RemoveRange(pedido.ItensPedido);
            _context.Pedidos.Remove(pedido);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}
