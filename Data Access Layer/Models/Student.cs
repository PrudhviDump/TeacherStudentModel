    namespace Backend.Data_Access_Layer.Models
{
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

        public class Student
        {
        [Key]
        [Column(TypeName = "varchar(10)")]
        public string StudentId { get; set; }   // A001, random

        [Required]
        [MinLength(3)]
        [Column(TypeName = "varchar(100)")]
        public string Name { get; set; }

        [Required]
        [Column(TypeName = "varchar(10)")]
        public string Gender { get; set; }

        [Required]
        [EmailAddress]
        [Column(TypeName = "varchar(150)")]
        public string EmailId { get; set; }

        // 🔑 FILE STORED IN DATABASE
        [Required]
        public byte[] IdProof { get; set; }

        [Required]
        [Column(TypeName = "varchar(255)")]
        public string IdProofFileName { get; set; }

        [Required]
        [Column(TypeName = "varchar(100)")]
        public string IdProofContentType { get; set; }
    }
}
