using System.ComponentModel.DataAnnotations;

namespace LibraryManagementSystem.Models
{
    public class Category
    {
        [Key]
        [Required(ErrorMessage = "Category ID is required.")]
        public int CategoryId { get; set; }
        [Required(ErrorMessage = "Name is required.")]
        public string Name { get; set; }
        public string? Description { get; set; }
        [DataType(DataType.DateTime)]
        public DateTime? CreatedDate { get; set; }
        [DataType(DataType.DateTime)]
        public DateTime? UpdatedDate { get; set; }
        public Boolean IsActive { get; set; }
        public string? Avatar { get; set; }

        public ICollection<Book>? Books { get; set; }
    }
}
