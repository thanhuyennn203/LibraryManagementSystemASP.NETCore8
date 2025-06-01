using System.ComponentModel.DataAnnotations;

namespace LibraryManagementSystem.Models
{
    public class Book
    {
        [Key]
        [Required(ErrorMessage = "Book ID is required.")]
        public int BookId { get; set; }
        [Required(ErrorMessage = "Title is required.")]
        [StringLength(200, ErrorMessage = "Book title can not exceed 200 characters.")]
        public string Title { get; set; }
        public string? Description { get; set; }

        [Required(ErrorMessage = "Book Code is required.")]
        public string BookCode { get; set; }
        public string? Publisher { get; set; }
        public DateTime? PublishedYear { get; set; }
        public int? CategoryId { get; set; }
        public int? AuthorId { get; set; }
        public int? TotalCopies { get; set; }
        public int? AvailableCopies { get; set; }
        public DateTime? CreatedDate { get; set; }
        public string? Avatar { get; set; }
        public string? Pdf { get; set; }

        public bool? IsDeleted { get; set; }
        public Author? Author { get; set; }
        public Category? Category { get; set; }

        public ICollection<Loan>? Loans { get; set; }

    }
}
