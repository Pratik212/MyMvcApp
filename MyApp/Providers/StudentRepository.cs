using Microsoft.EntityFrameworkCore;
using MyApp.Data;
using MyApp.Interfaces;
using MyApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyApp.Providers
{
    public class StudentRepository : IStudentRepository
    {
        private readonly MyContext _context;

        public StudentRepository(MyContext context)
        {
            _context = context;
        }

        public async Task<Student> AddStudent(Student student)
        {
            var studObj = await _context.Students.AddAsync(student);
            _context.SaveChanges();
            return studObj.Entity;
        }

        public async Task<IEnumerable<Student>> GetAll()
        {
            return await _context.Students.ToListAsync();
        }

        public async Task<Student> GetStudentById(int StudentId)
        {
            return await _context.Students.FirstOrDefaultAsync(x => x.StudentId == StudentId);
        }

        public async Task SaveChanges()
        {
            await _context.SaveChangesAsync();
        }
    }
}
