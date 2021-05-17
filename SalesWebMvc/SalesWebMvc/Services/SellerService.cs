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

        // Retorna todos os vendedores
        public List<Seller> FindAll()
        { return _contex.Seller.ToList(); }

        // Método para inserir os dados no banco de dados
        public void Insert(Seller obj)
        {            
            _contex.Add(obj);

            _contex.SaveChanges();
        }

        // Buscar um vendedor pelo seu número de identificação
        public Seller FindById(int id)
        { return _contex.Seller.Include(s => s.Department).FirstOrDefault(s => s.Id == id); }

        // Remover um vendedor
        public void Remove(int id)
        {
            // Buscar o vendedor
            var seller = _contex.Seller.Find(id);

            // Remover ele
            _contex.Seller.Remove(seller);

            // Aplicar essa mudança no banco de dados
            _contex.SaveChanges();
        }

        // Atualizar as informações do vendedor
        public void Update(Seller seller)
        {
            // Buscar no banco de dados se existe algum vendedor com esse número de identificação
            if(!_contex.Seller.Any(s => s.Id == seller.Id))
            {
                throw new NotFoundException("ERRO! Número de identificação (Id) não encontrado");
            }

            // Possibilidade de ocorrer uma exceção
            try
            {
                // Atualizar o vendedor
                _contex.Update(seller);

                // Atualizar o banco de dados
                _contex.SaveChanges();
            }

            catch(DbUpdateConcurrencyException e)
            {
                throw new DbConcurrencyException(e.Message);
            }           
        }
    }
}
