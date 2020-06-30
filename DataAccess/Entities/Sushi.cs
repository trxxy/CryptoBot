namespace DataAccess.Entities
{
    public class Sushi : Entity
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string ImageUrl { get; set; }
        public decimal Weight { get; set; }
        public decimal Price { get; set; }
    }
}