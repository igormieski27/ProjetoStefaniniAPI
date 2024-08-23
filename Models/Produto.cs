using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace PedidosApi.Models
{
    public class Produto
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(20)]
        public string NomeProduto { get; set; }

        [Required]
        [Column(TypeName = "decimal(10,2)")]
        public decimal Valor { get; set; }

        [JsonIgnore]
        public ICollection<ItensPedido> ItensPedido { get; set; } = new List<ItensPedido>();
    }
}
