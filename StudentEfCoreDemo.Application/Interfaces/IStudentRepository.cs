using StudentEfCoreDemo.Domain.Entities;

namespace StudentEfCoreDemo.Application.Interfaces
{
    public interface IStudentRepository
    {
        Task<IEnumerable<Student>> GetAllAsync();
        Task<Student?> GetByIdAsync(int id);
        Task<Student> AddAsync(Student student);
        Task UpdateAsync(Student student);
        Task DeleteAsync(int id);
        Task<bool> ExistsAsync(int id);
    }
} 