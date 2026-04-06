namespace Cat_API_Project.DTO
{
    public class BreedFactQueryParametersDTO
    {
        //Query DTO for URL filtering, pagination and sorting
        public int? BreedId { get; set; }
        public string? SortBy { get; set; }
        public string? SortOrder { get; set; }
        public int PageNumber { get; set; } = 1;
        public int PageSize { get; set; } = 10;
    }
}
