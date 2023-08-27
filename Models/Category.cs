using System.ComponentModel.DataAnnotations;

namespace BookShopWeb.Models
{
    public class Category
    {
        [Key]
        public Guid Id { get; set; }
        public string Name { get; set; }
        public int DisplayOrder { get; set; }
    }
}
