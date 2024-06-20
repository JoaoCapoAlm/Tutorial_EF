using Loja.Data;
using Loja.Data.Dtos;
using Loja.Data.ViewModels;
using Loja.Models;
using Loja.Utils;
using Microsoft.EntityFrameworkCore;

namespace Loja.Services
{
    public class ClienteService
    {
        private readonly LojaDbContext _context;
        public ClienteService(LojaDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<ClienteVM>> GetAllClientAsync()
        {
            return await _context.Cliente.Select(x => new ClienteVM(x)).ToArrayAsync();
        }

        public async Task<ClienteVM?> GetClientByIdAsync(int id)
        {
            return await _context.Cliente
                .Where(x => x.Id.Equals(id))
                .Select(x => new ClienteVM(x))
                .FirstOrDefaultAsync();
        }

        public async Task<ClienteVM> AddClientAsync(ClienteDto dto)
        {
            var cliente = await _context.Cliente.AddAsync(new Cliente()
            {
                Cpf = dto.Cpf,
                Email = dto.Email,
                Nome = dto.Nome,
                Password = dto.Password
            });
            await _context.SaveChangesAsync();

            var clienteVm = new ClienteVM(cliente.Entity);

            return clienteVm;
        }

        public async Task UpdateClienteAsync(int id, ClienteDto dto)
        {

            var client = await _context.Cliente.FirstAsync(x => x.Id.Equals(id));
            if (client == null)
                throw new KeyNotFoundException();

            client.Cpf = dto.Cpf;
            client.Email = dto.Email;
            client.Nome = dto.Nome;

            await _context.SaveChangesAsync();
        }

        public async Task DeleteClienteAsync(int id)
        {
            var client = await _context.Cliente.FindAsync(id);
            if (client != null)
            {
                _context.Cliente.Remove(client);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<string> LoginAsync(string email, string password)
        {
            var userId = await _context.Cliente
                .Where(x => x.Email.Equals(email)
                    && x.Password.Equals(password))
                .Select(x => x.Id)
                .FirstOrDefaultAsync();

            return TokenUtil.GenerateToken(userId.ToString());
        }
    }
}
