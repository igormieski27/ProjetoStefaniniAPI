using System.Collections.Generic;

namespace PedidosApi.Models
{
    public class RespostaPedido
    {
        public int Id { get; set; }
        public string NomeCliente { get; set; }
        public string EmailCliente { get; set; }
        public bool Pago { get; set; }
        public decimal ValorTotal { get; set; }
        public List<RespostaItemPedido> ItensPedido { get; set; }
    }

    public class RespostaItemPedido
    {
        public int Id { get; set; }
        public int IdProduto { get; set; }
        public string NomeProduto { get; set; }
        public decimal ValorUnitario { get; set; }
        public int Quantidade { get; set; }
    }
}