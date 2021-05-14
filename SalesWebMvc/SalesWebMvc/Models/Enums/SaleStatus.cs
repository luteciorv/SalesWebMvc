namespace SalesWebMvc.Models.Enums
{
    // Situação/estado da venda
    public enum SaleStatus : int
    {
        Pending = 0,  // Pendente
        Billed = 1,  // Faturado
        Canceled = 2 // Cancelado
    }
}