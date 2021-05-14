namespace SalesWebMvc.Models.Enums
{
    // Situação/estado da venda
    public enum SaleStatus : int
    {
        Peding = 0,  // Pendente
        Billed = 1,  // Faturado
        Canceled = 2 // Cancelado
    }
}