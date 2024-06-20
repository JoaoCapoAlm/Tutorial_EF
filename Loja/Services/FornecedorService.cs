using Loja.Data;
using Loja.Data.Dtos;
using Loja.Models;
using Microsoft.EntityFrameworkCore;

namespace Loja.Services
{
    public class FornecedorService
    {
        private readonly LojaDbContext _context;
        public FornecedorService(LojaDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Fornecedor>> GetAllFornecedorAsync()
        {
            return await _context.Fornecedor.ToArrayAsync();
        }

        public async Task<Fornecedor?> GetFornecedorByIdAsync(int id)
        {
            return await _context.Fornecedor.FindAsync(id);
        }

        public async Task<Fornecedor> AddFornecedorAsync(FornecedorDto dto)
        {
            var fornecedor = await _context.Fornecedor.AddAsync(new Fornecedor()
            {
                Cnpj = dto.Cnpj,
                Email = dto.Email,
                Nome = dto.Nome,
                Telefone = dto.Telefone
            });
            await _context.SaveChangesAsync();

            return fornecedor.Entity;
        }

        public async Task UpdateFornecedorAsync(int id, FornecedorDto dto)
        {

            var fornecedor = await _context.Fornecedor.FirstAsync(x => x.Id.Equals(id));
            if (fornecedor == null)
                throw new KeyNotFoundException();

            fornecedor.Cnpj = dto.Cnpj;
            fornecedor.Email = dto.Email;
            fornecedor.Nome = dto.Nome;
            fornecedor.Telefone = dto.Telefone;

            await _context.SaveChangesAsync();
        }

        public async Task DeleteFornecedorAsync(int id)
        {
            var fornecedor = await _context.Fornecedor.FindAsync(id);
            if (fornecedor != null)
            {
                _context.Fornecedor.Remove(fornecedor);
                await _context.SaveChangesAsync();
            }
        }
    }
}
