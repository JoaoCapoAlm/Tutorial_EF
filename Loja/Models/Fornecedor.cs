using System.ComponentModel.DataAnnotations;

namespace Loja.Models
{
    public class Fornecedor
    {
        [Key]
        public int Id { get; set; }
        public string Cnpj { get; set; }
        public string Nome { get; set; }
        public string Endereco { get; set; }
        [EmailAddress]
        public string Email { get; set; }
        [Phone]
        public string Telefone { get; set; }
        public virtual ICollection<Produto> Produtos { get; set; }
    }
}
