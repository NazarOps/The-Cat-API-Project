namespace Cat_API_Project.DTO
{
    public class BreedQueryParametersDTO
    {
        public string? Origin { get; set; }
        public string? SortBy { get; set; }
        public string? SortOrder { get; set; } = "asc";
        public int PageNumber { get; set; } = 1;
        public int PageSize { get; set; } = 10;
    }
}
