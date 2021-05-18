using Microsoft.EntityFrameworkCore;
using SalesWebMvc.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SalesWebMvc.Services
{
    public class SalesRecordService
    {
        // Criar dependência para o SalesWebMvcContext
        private readonly SalesWebMvcContext _contex;

        // Contrutor #1
        public SalesRecordService(SalesWebMvcContext contex)
        {
            _contex = contex;
        }

        public async Task<List<SalesRecord>> FindByDateAsync(DateTime? minDate, DateTime? maxDate)
        {
            var result = from obj in _contex.SalesRecord select obj;

            // Caso a variável "Data Mínima" tenha um valor
            if (minDate.HasValue)
            {
                result = result.Where(x => x.Date >= minDate.Value);
            }

            // Caso a variável "Data Máxima" tenha um valor
            if (maxDate.HasValue)
            {
                result = result.Where(x => x.Date <= maxDate.Value);
            }

            return await result.Include(x => x.Seller).Include(x => x.Seller.Department)
                   .OrderByDescending(x => x.Date).ToListAsync();
        }
    }
}
