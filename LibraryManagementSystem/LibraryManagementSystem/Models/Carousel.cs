using System.ComponentModel.DataAnnotations;

namespace LibraryManagementSystem.Models
{
    public class Carousel
    {
        [Key]
        [Required(ErrorMessage = "Carousel ID is required.")]
        public int CarouselId { get; set; }

        [Required(ErrorMessage = "ImageUrl is required.")]
        [Url(ErrorMessage = "Invalid URL.")]
        public string ImageUrl { get; set; }

        [Required(ErrorMessage = "Title is required.")]
        [StringLength(200, ErrorMessage = "Title cannot exceed 200 characters.")]
        public string Title { get; set; }

        public string? Description { get; set; }

        [Url(ErrorMessage = "Invalid Link URL.")]
        public string? LinkUrl { get; set; }

        [Required(ErrorMessage = "Order is required.")]
        public int Order { get; set; }

        public Boolean? IsActive { get; set; }
        [DataType(DataType.DateTime)]
        public DateTime? CreatedDate { get; set; }
        [DataType(DataType.DateTime)]
        public DateTime? UpdatedDate { get; set; }

    }
}
