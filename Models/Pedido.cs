using System.ComponentModel.DataAnnotations;

namespace PedidosApi.Models
{
    public class Pedido
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(60)]
        public string NomeCliente { get; set; }

        [Required]
        [StringLength(60)]
        public string EmailCliente { get; set; }

        [Required]
        public DateTime DataCriacao { get; set; }

        [Required]
        public bool Pago { get; set; }


        public ICollection<ItensPedido> ItensPedido { get; set; } = new List<ItensPedido>();
    }
}
