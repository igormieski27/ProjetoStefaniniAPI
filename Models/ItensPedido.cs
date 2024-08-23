using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace PedidosApi.Models
{
    public class ItensPedido
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public int IdPedido { get; set; }

        [Required]
        public int IdProduto { get; set; }

        [Required]
        public int Quantidade { get; set; }

        [JsonIgnore]
        public Pedido Pedido { get; set; }
        [JsonIgnore]
        public Produto Produto { get; set; }
    }
}
