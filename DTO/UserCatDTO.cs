namespace Cat_API_Project.DTO
{
    public class UserCatDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int BreedId { get; set; }
        public string? ImageUrl { get; set; }
    }
}
