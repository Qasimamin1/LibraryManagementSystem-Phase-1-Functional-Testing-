using System.ComponentModel.DataAnnotations;

namespace LibraryManagementSystemTesting.Models
{
    public class BooksAdmin
    {
        [Key]
        public int BookId { get; set; }

        [Required]
        [StringLength(100)]
        public string BookCode { get; set; }

        [Required]
        [StringLength(200)]
        public string BookName { get; set; }

        [Required]
        public double BookPrice { get; set; }
    }
}
