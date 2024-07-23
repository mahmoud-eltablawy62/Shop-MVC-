using Microsoft.VisualBasic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ShopMvc.Models
{
    public class Category
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public int Category_Id { get; set; }
        [Required]
        public string Category_Name { get; set; }
        [MaxLength(100)]          
        public string Catogory_Description { get; set; }     
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    }
}
