using System.ComponentModel.DataAnnotations;

namespace Backend.Business_Logic_Layer.DTO
{
    public class UpdateStudentDTO
    {
        [Required]
        [MinLength(3)]
        public string Name { get; set; }

        [Required]
        [RegularExpression("Male|Female|Other")]
        public string Gender { get; set; }

        [Required]
        [EmailAddress]
        public string EmailId { get; set; }

        // Optional during update
        public IFormFile? IdProof { get; set; }
    }
}
