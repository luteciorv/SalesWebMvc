using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;

namespace SalesWebMvc.Models
{
    public class Seller
    {
        // Informações do vendedor
        public int Id { get; set; }              // Número de identificação

        [Required(ErrorMessage = "Campo '{0}' requerido")] // Campo obrigatório
        [StringLength(60, MinimumLength = 3, ErrorMessage = "Tamanho do nome precisa estar entre {1} e {2}")] // Tamanho do nome (Entre 3 e 60)
        [Display(Name = "Nome")]
        public string Name { get; set; }         // Nome

        [Required(ErrorMessage = "Campo '{0}' requerido")] // Campo obrigatório
        [EmailAddress(ErrorMessage = "Entre com um email válido")] // Campo obrigatório
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }        // Email

        //[DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
        [Required(ErrorMessage = "Campo '{0}' requerido")] // Campo obrigatório
        [Display(Name = "Data de nascimento")]
        [DataType(DataType.Date)]        
        public DateTime BirthDate { get; set; }  // Data de nascimento

        [Required(ErrorMessage = "Campo '{0}' requerido")] // Campo obrigatório
        [Display(Name = "Salário base")]
        [DisplayFormat(DataFormatString = "{0:F2}")]
        [Range(100.00f, 500000.00f, ErrorMessage = "O valor {0} precisa estar entre {1} e {2}")]        
        public float BaseSalary { get; set; }    // Valor do salário base
       
        [Display(Name = "Departamento")]
        public Department Department { get; set; } // Departamento

        [Display(Name = "Nome do departamento")]
        public int DepartmentId { get; set; } // Número de identificação do departamento

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
