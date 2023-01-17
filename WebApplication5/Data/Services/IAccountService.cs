using WebApplication5.Models;

namespace WebApplication5.Data.Services
{
    public interface IAccountService
    {
        Task Register(Student student);

        Task RegisterTeacher(Teacher teacher);
        Task EditUser(string email, Student student);
    }
}
