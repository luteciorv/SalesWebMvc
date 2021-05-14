using System.Collections.Generic;

namespace SalesWebMvc.Models.ViewModels
{
    public class SellerFormViewModel
    {
        // Informações do formulário de cadastro de vendedor
        public Seller Seller { get; set; }
        public ICollection<Department> Departments { get; set; }
    }
}
