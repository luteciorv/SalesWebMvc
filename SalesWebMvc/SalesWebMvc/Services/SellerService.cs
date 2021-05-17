using SalesWebMvc.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using SalesWebMvc.Services.Exceptions;

namespace SalesWebMvc.Services
{
    public class SellerService
    {
        // Criar dependência para o SalesWebMvcContext
        private readonly SalesWebMvcContext _contex;

        // Contrutor #1
        public SellerService(SalesWebMvcContext contex)
        {
            _contex = contex;
        }

        // Retorna todos os vendedores // ASSÍNCRONO
        public async Task<List<Seller>> FindAllAsync()
        { 
            return await _contex.Seller.ToListAsync(); 
        }

        // Método para inserir os dados no banco de dados // ASSÍNCRONO
        public async Task InsertAsync(Seller obj)
        {            
            _contex.Add(obj);

            await _contex.SaveChangesAsync();
        }

        // Buscar um venedor pelo seu número de identificação // ASSÍNCRONO
        public async Task<Seller> FindByIdAsync(int id)
        { 
            return await _contex.Seller.Include(s => s.Department).FirstOrDefaultAsync(s => s.Id == id); 
        }

        // Remover um vendedor // ASSÍNCRONO
        public async Task RemoveAsync(int id)
        {
            try
            {
                // Buscar o vendedor
                var seller = await _contex.Seller.FindAsync(id);

                // Remover ele
                _contex.Seller.Remove(seller);

                // Aplicar essa mudança no banco de dados
                await _contex.SaveChangesAsync();
            }

            catch(DbUpdateException e)
            {
                throw new IntegrityException("Não é possível deletar o vendedor que ainda possui vendas.");
            }
            
        }

        // Atualizar as informações do vendedor // ASSÍNCRONO
        public async Task UpdateAsync(Seller seller)
        {
            // Buscar no banco de dados se existe algum vendedor com esse número de identificação
            bool hasAny = await _contex.Seller.AnyAsync(s => s.Id == seller.Id);
            
            if (!hasAny)
            {
                throw new NotFoundException("ERRO! Número de identificação (Id) não encontrado");
            }

            // Possibilidade de ocorrer uma exceção
            try
            {
                // Atualizar o vendedor
                _contex.Update(seller);

                // Atualizar o banco de dados
                await _contex.SaveChangesAsync();
            }

            catch(DbUpdateConcurrencyException e)
            {
                throw new DbConcurrencyException(e.Message);
            }           
        }
    }
}
