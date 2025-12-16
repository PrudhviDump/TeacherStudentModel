using System.ComponentModel.DataAnnotations;

namespace Backend.Business_Logic_Layer.DTO
{
    public class CreateStudentDTO
    {
        [Required]
        [MinLength(3, ErrorMessage = "Name must be at least 3 characters")]
        public string Name { get; set; }

        [Required]
        [RegularExpression("Male|Female|Other",
            ErrorMessage = "Gender must be Male, Female, or Other")]
        public string Gender { get; set; }

        [Required]
        [EmailAddress(ErrorMessage = "Invalid email address")]
        public string EmailId { get; set; }

        [Required(ErrorMessage = "ID Proof is required")]
        public IFormFile IdProof { get; set; }
    }
}
