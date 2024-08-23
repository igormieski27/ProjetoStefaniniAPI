using System.ComponentModel.DataAnnotations;

public class CreatePedidoDto
{

    public int? Id { get; set; }

    [Required]
    [StringLength(60)]
    public string NomeCliente { get; set; }

    [Required]
    [StringLength(60)]
    public string EmailCliente { get; set; }

    [Required]
    public bool Pago { get; set; }

    [Required]
    public List<ItemPedidoDto> Itens { get; set; }
}

public class ItemPedidoDto
{
    [Required]
    public int IdProduto { get; set; }

    [Required]
    public int Quantidade { get; set; }
}
