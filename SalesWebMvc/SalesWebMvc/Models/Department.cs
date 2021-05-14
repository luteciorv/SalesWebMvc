using System;
using System.Collections.Generic;
using System.Linq;

namespace SalesWebMvc.Models
{
    public class Department
    {
        // Informações do departamento
        public int Id { get; set; } // Número de identificação
        public string Name { get; set; } // Nome
        public ICollection<Seller> Sellers { get; set; } = new List<Seller>(); // Lista contendo os vendedores

        // Construtor #1 - Default
        public Department()
        {

        }

        // Construtor #2
        public Department(int id, string name)
        {
            Id = id;
            Name = name;
        }

        // Adiciona um vendedor na lista de vendedores do departamento
        public void AddSeller(Seller seller)
        { Sellers.Add(seller); }


        // Total de vendas de todos os vendedores do departamento entre os períodos informados
        public float TotalSales(DateTime initialDate, DateTime finalDate)
        { return Sellers.Sum(s => s.TotalSales(initialDate, finalDate)); }
    }
}
