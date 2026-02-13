namespace PruebaTecnica.Common.Core.Filter
{
    public class BaseFilter
    {
        public int Skip { get; set; } = 0;
        public int Take { get; set; } = 50;
        public string? Search { get; set; }
        public int? Id { get; set; }
    }
}