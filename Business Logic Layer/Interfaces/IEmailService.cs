namespace Backend.Business_Logic_Layer.Interfaces
{
    public interface IEmailService
    {
        Task SendStudentEnrollmentEmailAsync(string toEmail, string studentName);
    }
}
