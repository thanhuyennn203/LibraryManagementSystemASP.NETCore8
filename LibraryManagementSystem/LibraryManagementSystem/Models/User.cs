using System.ComponentModel.DataAnnotations;

namespace LibraryManagementSystem.Models
{
    public class User
    {
        [Key]
        [Required(ErrorMessage = "User ID is required.")]
        public int UserId { get; set; }
        [StringLength(200, ErrorMessage = "Fullname cannot exceed 200 characters.")]
        public string Fullname { get; set; }
        public string Description { get; set; }
        [Required(ErrorMessage = "Password is required.")]
        public string Password { get; set; }
        [Required(ErrorMessage = "Email is required.")]
        [EmailAddress(ErrorMessage = "Invalid Email Address.")]
        public string Email { get; set; }

        [Phone(ErrorMessage = "Invalid Phone Number.")]
        public int Phone { get; set; }
        public string Address { get; set; }
        public int Status { get; set; }
        [DataType(DataType.DateTime)]
        public DateTime CreatedDate { get; set; }
        public string UserCode { get; set; }
        public Boolean IsLocked { get; set; }
        public Boolean IsDeleted { get; set; }
        public Boolean IsActive { get; set; }
        public string ActiveCode { get; set; }
        public string Avatar { get; set; }
        public ICollection<Loan> Loans { get; set; }

    }
}
