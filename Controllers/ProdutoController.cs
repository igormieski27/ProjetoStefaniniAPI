using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PedidosApi.Data;
using PedidosApi.Models;
using Swashbuckle.AspNetCore.Annotations;

[ApiController]
[Route("api/[controller]")]
public class ProdutoController : ControllerBase
{
    private readonly AppDbContext _context;

    public ProdutoController(AppDbContext context)
    {
        _context = context;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<Produto>>> GetProdutos()
    {
        return await _context.Produtos.ToListAsync();
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<Produto>> GetProduto(int id)
    {
        var produto = await _context.Produtos.FindAsync(id);

        if (produto == null)
        {
            return NotFound();
        }

        return Ok(produto);
    }

    [HttpPost]
    public async Task<ActionResult<Produto>> PostProduto([FromBody] Produto produto)
    {
        if (produto == null)
        {
            return BadRequest("Produto não pode ser nulo.");
        }

        // Verifica se o produto já existe
        var existingProduto = await _context.Produtos.FindAsync(produto.Id);

        if (existingProduto != null)
        {
            // Atualiza o produto existente
            existingProduto.NomeProduto = produto.NomeProduto;
            existingProduto.Valor = produto.Valor;
            // Atualize outras propriedades conforme necessário

            _context.Produtos.Update(existingProduto);
        }
        else
        {
            // Adiciona um novo produto
            _context.Produtos.Add(produto);
        }

        await _context.SaveChangesAsync();

        if (existingProduto != null)
        {
            // Retorna o produto atualizado
            return Ok(existingProduto);
        }
        else
        {
            // Retorna o produto recém-criado
            return CreatedAtAction("GetProduto", new { id = produto.Id }, produto);
        }
    }
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteProduto(int id)
    {
        var produto = await _context.Produtos.FindAsync(id);
        if (produto == null)
        {
            return NotFound();
        }

        // Remover todos os itens de pedido que referenciam este produto
        var itensPedidos = _context.ItensPedido.Where(i => i.IdProduto == id);
        _context.ItensPedido.RemoveRange(itensPedidos);

        // Remover o produto
        _context.Produtos.Remove(produto);

        await _context.SaveChangesAsync();

        return NoContent();
    }

    private bool ProdutoExists(int id)
    {
        return _context.Produtos.Any(e => e.Id == id);
    }
}
