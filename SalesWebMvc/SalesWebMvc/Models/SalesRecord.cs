using System;
using System.ComponentModel.DataAnnotations;
using SalesWebMvc.Models.Enums;

namespace SalesWebMvc.Models
{
    public class SalesRecord
    {
        // Informações do registro de venda
        public int Id { get; set; }            // Número de identificação

        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime Date { get; set; }     // Data do registro da venda

        [DisplayFormat(DataFormatString = "R$ {0:F2}")]
        public double SaleValue { get; set; }        // Valor da venda
        public SaleStatus Status { get; set; } // Estado atual da venda        
        public Seller Seller { get; set; } // Vendedor

        // Construtor #1 - Default
        public SalesRecord()
        {

        }

        // Construtor #2
        public SalesRecord(int id, DateTime date, double saleValue, SaleStatus status, Seller seller)
        {
            Id = id;
            Date = date;
            SaleValue = saleValue;
            Status = status;
            Seller = seller;
        }
    }
}
