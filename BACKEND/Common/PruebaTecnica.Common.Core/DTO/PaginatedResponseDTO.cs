namespace PruebaTecnica.Common.Core.DTO
{
    public class PaginatedResponseDTO:BaseResponseDTO
    {
         
        public int StatusCode { get; set; }
        public string Mensaje { get; set; }
        public bool Confirmacion { get; set; }
        public object Data { get; set; }
        
         public int? Total { get; set; }  // Nullable for pagination
        public int? Page { get; set; }   // Nullable for pagination
        public int? Pages { get; set; } // Nullable for pagination
  
    }
}