using Backend.Data_Access_Layer.Models;

namespace Backend.Data_Access_Layer.Interface
{
    public interface IStudentRepo
    {
        Task<IEnumerable<Student>> GetAllStudentsAsync();
        Task<Student> GetStudentByIdAsync(string studentId);
        Task AddStudentAsync(Student student);
        Task UpdateStudentAsync(Student student);
        Task DeleteStudentAsync(string studentId);
    }
}
