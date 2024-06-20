namespace Loja.Data.ViewModels
{
    public record VendaProdutoDetalhadaVM(int idVenda, int idProduto, string nomeProduto, DateTime dataVenda, string nomeCliente, int qtdProduto, double precoVenda);

    public record VendaProdutoSumarizadoVM(int idProduto, string nomeProduto, int qtdVendas, int qtdUnidadesVendidas, double somaValorCobrado);

    public record VendaClienteSumarizadoVM(int idCliente, string nomeCliente, int qtdVendas, IEnumerable<VendaProdutoSumarizadoVM> produtos, double somaValorCobrado);

    public record VendaClienteDetalhadaVM(int idVenda, int idCliente, string nomeCliente, DateTime dataVenda, string nomeProduto, int qtdProduto, double precoVenda);
}
