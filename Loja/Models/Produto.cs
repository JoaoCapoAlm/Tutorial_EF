namespace Loja.Models
{
    public class Produto
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public double Preco { get; set; }
        public virtual Fornecedor Fornecedor { get; set; }
        public int? FornecedorId { get; set; }
    }
}
