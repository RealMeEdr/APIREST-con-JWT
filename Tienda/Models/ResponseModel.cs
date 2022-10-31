namespace Tienda.Models
{
    public class ResponseModel
    {
        public bool IsSuccess { get; set; }
        public string MessageException { get; set; }
        public string MessageStackTrace { get; set; }
        public object ObjectResponse { get; set; }
        public string Message { get; set; }
    }
}
