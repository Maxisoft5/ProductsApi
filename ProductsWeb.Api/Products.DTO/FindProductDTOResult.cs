namespace Products.DTO
{
    public class FindProductDTOResult
    {
        public Guid ProductVersionId { get; set; }
        public string ProductName { get; set; }
        public string ProductVersion { get; set; }
        public decimal Width { get; set; }
        public decimal Length { get; set; }
        public decimal Height { get; set; }

    }
}
