using System.ComponentModel.DataAnnotations;

namespace Logic.DataTableObjects
{
    public class SushiDTO
    {
        public int Id { get; set; }
        [Key]
        public string Name { get; set; }
        public string Description { get; set; }
        public string ImageUrl { get; set; }
        public decimal Weight { get; set; }
        public decimal Price { get; set; }
    }
}