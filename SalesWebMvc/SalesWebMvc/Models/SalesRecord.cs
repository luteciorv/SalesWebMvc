using System;
using SalesWebMvc.Models.Enums;

namespace SalesWebMvc.Models
{
    public class SalesRecord
    {
        // Informações do registro de venda
        public int Id { get; set; }            // Número de identificação
        public DateTime Date { get; set; }     // Data do registro da venda
        public float SaleValue { get; set; }        // Valor da venda
        public SaleStatus Status { get; set; } // Estado atual da venda        
        public Seller Seller { get; set; } // Vendedor

        // Construtor #1 - Default
        public SalesRecord()
        {

        }

        // Construtor #2
        public SalesRecord(int id, DateTime date, float saleValue, SaleStatus status, Seller seller)
        {
            Id = id;
            Date = date;
            SaleValue = saleValue;
            Status = status;
            Seller = seller;
        }
    }
}
