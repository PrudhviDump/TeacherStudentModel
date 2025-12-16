using Backend.Data_Access_Layer.Interface;
using Backend.Data_Access_Layer.Models;
using Backend.Business_Logic_Layer.DTO;
using iTextSharp.text.pdf;
using Backend.Business_Logic_Layer.Interfaces;

namespace Backend.Business_Logic_Layer.Services
{
    public class StudentService
    {
        private readonly IStudentRepo _repo;
        private readonly IEmailService _emailService;

        public StudentService(IStudentRepo repo, IEmailService emailService)
        {
            _repo = repo;
            _emailService = emailService;
        }

        public StudentService(IStudentRepo repo)
        {
            _repo = repo;
        }

        // 🔹 Generate random unique StudentId (A001 - A999)
        private async Task<string> GenerateRandomStudentIdAsync()
        {
            var existingIds = (await _repo.GetAllStudentsAsync())
                                .Select(s => s.StudentId)
                                .ToHashSet();

            var availableIds = new List<string>();

            for (int i = 1; i <= 999; i++)
            {
                var id = "A" + i.ToString("D3");
                if (!existingIds.Contains(id))
                    availableIds.Add(id);
            }

            if (!availableIds.Any())
                throw new Exception("Maximum number of students reached.");

            return availableIds[new Random().Next(availableIds.Count)];
        }

        // 🔹 Validate & read ID proof file
        private async Task<(byte[] fileBytes, string fileName, string contentType)>
            ValidateAndReadFileAsync(IFormFile file)
        {
            if (file == null || file.Length == 0)
                throw new Exception("ID proof file is required.");

            if (file.Length > 2 * 1024 * 1024)
                throw new Exception("File size must be less than 2MB.");

            // Reject password-protected PDFs
            if (Path.GetExtension(file.FileName).ToLower() == ".pdf")
            {
                try
                {
                    using var stream = file.OpenReadStream();
                    var reader = new PdfReader(stream);
                    if (reader.IsEncrypted())
                        throw new Exception("Password-protected PDF files are not allowed.");
                }
                catch
                {
                    throw new Exception("Invalid or protected PDF file.");
                }
            }

            using var ms = new MemoryStream();
            await file.CopyToAsync(ms);

            return (ms.ToArray(), file.FileName, file.ContentType);
        }

        // ✅ CREATE
        public async Task<Student> AddStudentAsync(CreateStudentDTO dto)
        {
            // Validate and read the uploaded file
            var (bytes, fileName, contentType) = await ValidateAndReadFileAsync(dto.IdProof);

            // Create Student entity
            var student = new Student
            {
                StudentId = await GenerateRandomStudentIdAsync(),
                Name = dto.Name,
                Gender = dto.Gender,
                EmailId = dto.EmailId,
                IdProof = bytes,
                IdProofFileName = fileName,
                IdProofContentType = contentType
            };

            // Save student to database
            await _repo.AddStudentAsync(student);

            // ✅ Send enrollment email
            await _emailService.SendStudentEnrollmentEmailAsync(
                student.EmailId,
                student.Name
            );

            return student;
        }


        // ✅ READ ALL
        public async Task<IEnumerable<Student>> GetAllStudentsAsync()
        {
            return await _repo.GetAllStudentsAsync();
        }

        // ✅ READ BY ID
        public async Task<Student?> GetStudentByIdAsync(string studentId)
        {
            return await _repo.GetStudentByIdAsync(studentId);
        }

        // ✅ UPDATE
        public async Task UpdateStudentAsync(string studentId, UpdateStudentDTO dto)
        {
            var student = await _repo.GetStudentByIdAsync(studentId);
            if (student == null)
                throw new Exception("Student not found.");

            student.Name = dto.Name;
            student.Gender = dto.Gender;
            student.EmailId = dto.EmailId;

            // If file is re-uploaded
            if (dto.IdProof != null)
            {
                var (bytes, fileName, contentType) =
                    await ValidateAndReadFileAsync(dto.IdProof);

                student.IdProof = bytes;
                student.IdProofFileName = fileName;
                student.IdProofContentType = contentType;
            }

            await _repo.UpdateStudentAsync(student);
        }

        // ✅ DELETE
        public async Task DeleteStudentAsync(string studentId)
        {
            await _repo.DeleteStudentAsync(studentId);
        }
    }
}

