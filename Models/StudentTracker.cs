using System.ComponentModel.DataAnnotations;

namespace StudentManagement.Models
{
    public class StudentTracker
    {
        [Key]
        public int ID { get; set; }

        [Required]
        public string OperationLog { get; set; }
    }
}
