using BookShopWeb.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace BookShopWeb.DataAccess.ViewModels
{
    public class ProductVM
    {
        public Product Product { get; set; }
        [Required]
        public IEnumerable<SelectListItem> Categories { get; set; }
    }
}
