using System;

namespace SalesWebMvc.Models.ViewModels
{
    public class ErrorViewModel
    {
        // Informa��es da p�gina de erro
        public string RequestId { get; set; }
        public string Message { get; set; }

        public bool ShowRequestId => !string.IsNullOrEmpty(RequestId);
    }
}