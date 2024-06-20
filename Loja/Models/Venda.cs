using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Loja.Models
{
    public class Venda
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public DateTime DataHora { get; set; }
        [MaxLength(255)]
        public string NotaFiscal { get; set; }
        public virtual Cliente Cliente { get; set; }
        public int ClienteId { get; set; }
        public virtual Produto Produto { get; set; }
        public int ProdutoId { get; set; }
        public int QtdProduto { get; set; }
        public double PrecoUnitario { get; set; }
    }
}
