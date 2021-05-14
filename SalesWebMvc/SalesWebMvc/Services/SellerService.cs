using SalesWebMvc.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

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
    }
}
