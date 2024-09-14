using static Tickets.Models.ErrorMessages;
using Tickets.Models;

namespace Tickets.Data.DTO
{
    public class ResponseClient<T>
    {
        public bool Success { get; set; }
        public ErrorCode ErrorCode { get; set; }
        public string ErrorMessage { get; set; }
        public T Data { get; set; }
   
        public ResponseClient(bool success)
        {
            Success = success;
            ErrorCode = ErrorCode.OperacaoBemSucedida;
            ErrorMessage = string.Empty;
            Data = default;
        }
 
        public ResponseClient(bool success, T data)
        {
            Success = success;
            Data = data;
            ErrorCode = ErrorCode.OperacaoBemSucedida;
            ErrorMessage = ErrorMessages.GetErrorMessage(ErrorCode);
        }
   
        public ResponseClient(bool success, ErrorCode errorCode, string errorMessage = "")
        {
            Success = success;
            ErrorCode = errorCode;
            ErrorMessage = !string.IsNullOrEmpty(errorMessage) ? 
                           errorMessage : ErrorMessages.GetErrorMessage(errorCode);
            Data = default; 
        }
    }


}
