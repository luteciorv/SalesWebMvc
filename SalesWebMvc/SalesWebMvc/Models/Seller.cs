using System;
using System.Collections.Generic;
using System.Linq;

namespace SalesWebMvc.Models
{
    public class Seller
    {
        // Informações do vendedor
        public int Id { get; set; }              // Número de identificação
        public string Name { get; set; }         // Nome
        public string Email { get; set; }        // Email
        public DateTime BirthDate { get; set; }  // Data de nascimento
        public float BaseSalary { get; set; }    // Valor do salário base
        public Department Department { get; set; } // Departamento
        public ICollection<SalesRecord> SalesRecords { get; set; } = new List<SalesRecord>(); // Registro de vendas

        // Construtor #1 - Default
        public Seller()
        {

        }

        // Construtor #2
        public Seller(int id, string name, string email, DateTime birthDate, float baseSalary, Department department)
        {
            Id = id;
            Name = name;
            Email = email;
            BirthDate = birthDate;
            BaseSalary = baseSalary;
            Department = department;
        }

        // Adiciona um registro de venda na lista dos registros de vendas
        public void AddSales(SalesRecord salesRecord)
        { SalesRecords.Add(salesRecord); }

        // Remove um registro de venda na lista dos registros de vendas
        public void RemoveSales(SalesRecord salesRecord)
        { SalesRecords.Remove(salesRecord); }

        // Total de vendas do vendedor entre os períodos informados
        public float TotalSales(DateTime initialDate, DateTime finalDate)
        { return SalesRecords.Where(sR => sR.Date >= initialDate && sR.Date <= finalDate).Sum(sr => sr.SaleValue); }       
    }
}
