using System;

namespace SalesWebMvc.Models.ViewModels
{
    public class ErrorViewModel
    {
        // Informações da página de erro
        public string RequestId { get; set; }
        public string Message { get; set; }

        public bool ShowRequestId => !string.IsNullOrEmpty(RequestId);
    }
}