using Backend.Data_Access_Layer.Context;
using Backend.Data_Access_Layer.Interface;
using Backend.Data_Access_Layer.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Backend.Data_Access_Layer.Implementation
{
    public class StudentRepo : IStudentRepo
    {
        private readonly StudentDbContext _context;

        public StudentRepo(StudentDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Student>> GetAllStudentsAsync()
        {
            return await _context.Students.ToListAsync();
        }

        public async Task<Student> GetStudentByIdAsync(string studentId)
        {
            return await _context.Students.FindAsync(studentId);
        }

        public async Task AddStudentAsync(Student student)
        {
            await _context.Students.AddAsync(student);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateStudentAsync(Student student)
        {
            _context.Students.Update(student);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteStudentAsync(string studentId)
        {
            var student = await _context.Students.FindAsync(studentId);
            if (student != null)
            {
                _context.Students.Remove(student);
                await _context.SaveChangesAsync();
            }
        }
    }
}
