using MyApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyApp.Interfaces
{
    public interface IStudentRepository
    {
        Task<IEnumerable<Student>> GetAll();

        Task<Student> AddStudent(Student student);

        Task<Student> GetStudentById(int StudentId);

        Task SaveChanges();
    }
}
