using System.ComponentModel.DataAnnotations;

namespace LibraryManagementSystem.Models
{
    public class Author
    {
        [Key]
        [Required(ErrorMessage = "Author ID is required.")]
        public int AuthorId { get; set; }

        [Required]
        [StringLength(100)]
        public string FirstName { get; set; }

        [StringLength(100)]
        public string? LastName { get; set; }
        [DataType(DataType.DateTime)]
        public DateTime? DateOfBirth { get; set; }
        public string? Biography { get; set; }
        [StringLength(100)]
        public string? Nationality { get; set; }
        [StringLength(100)]
        [EmailAddress(ErrorMessage = "Invalid Email Address.")]
        public string? Email { get; set; }
        [StringLength(100)]
        public string? Website { get; set; }
        [DataType(DataType.DateTime)]
        public DateTime? CreatedDate { get; set; }
        public Boolean IsActive { get; set; }
        public string? Avatar { get; set; }

        public ICollection<Book>? Books { get; set; }
    }
}
