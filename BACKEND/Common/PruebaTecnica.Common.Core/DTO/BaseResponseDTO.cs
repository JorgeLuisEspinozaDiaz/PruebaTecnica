namespace PruebaTecnica.Common.Core.DTO
{
    public class BaseResponseDTO
    {
         public BaseResponseDTO()
        {

            Confirmacion = false;
            Mensaje = String.Empty;
        }
        public int StatusCode { get; set; }
        public string Mensaje { get; set; }
        public bool Confirmacion { get; set; }
        public object Data { get; set; }
    }
}