using Loja.Models;

namespace Loja.Data.ViewModels
{
    public class ClienteVM
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public string Cpf { get; set; }
        public string Email { get; set; }

        public ClienteVM(Cliente cliente)
        {
            Id = cliente.Id;
            Nome = cliente.Nome;
            Cpf = cliente.Cpf;
            Email = cliente.Email;
        }
    }
}
