using System.ComponentModel.DataAnnotations;

namespace LibraryManagementSystem.Models
{
    public class Loan
    {
        [Key]
        [Required(ErrorMessage = "Loan ID is required.")]
        public int LoanId { get; set; }
        [Required(ErrorMessage = "User ID is required.")]
        public int UserId { get; set; }
        [Required(ErrorMessage = "Book ID is required.")]
        public int BookId { get; set; }
        [DataType(DataType.DateTime)]
        [Required(ErrorMessage = "Loan Date is required.")]
        public DateTime? LoanDate { get; set; }
        [DataType(DataType.DateTime)]
        public DateTime? DueDate { get; set; }
        [DataType(DataType.DateTime)]
        public DateTime? ReturnDate { get; set; }
        public int Status { get; set; }
        public User? User { get; set; }
        public Book? Book { get; set; }
    }
}
