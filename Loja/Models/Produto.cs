using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Loja.Models
{
    public class Produto
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string Nome { get; set; }
        public double Preco { get; set; }
        public virtual Fornecedor Fornecedor { get; set; }
        public int? FornecedorId { get; set; }
        public virtual IEnumerable<Venda> Vendas { get; set; }
    }
}
