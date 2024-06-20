namespace Loja.Data.Dtos
{
    public record VendaDto(
        DateTime DataHora,
        string NotaFiscal,
        int ClienteId,
        int ProdutoId,
        int QtdProduto,
        double PrecoUnitario
    );
}
