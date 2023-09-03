using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BookShopWeb.Models
{
    [Table("Category")]
    public class Category
    {
        [Key]

        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Range(1, 100)]
        public int DisplayOrder { get; set; }
    }
}
