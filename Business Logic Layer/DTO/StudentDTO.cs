using System.ComponentModel.DataAnnotations;

namespace Backend.Business_Logic_Layer.DTO
{
    public class StudentDTO
    {
        public string StudentId { get; set; }

        public string Name { get; set; }

        public string Gender { get; set; }

        public string EmailId { get; set; }
    }
}
