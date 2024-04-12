using System.ComponentModel.DataAnnotations;

namespace WaiterChefBoss.Models
{
    public class CategoryViewModelService
    {
        [Required]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public int Status { get; set; }
    }
}
