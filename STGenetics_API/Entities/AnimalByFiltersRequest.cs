namespace STGenetics_API.Entities
{
    public class AnimalByFiltersRequest
    {
        public int Animal_Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Sex { get; set; } = string.Empty;
        public string Status { get; set; } = string.Empty;
    }
}
