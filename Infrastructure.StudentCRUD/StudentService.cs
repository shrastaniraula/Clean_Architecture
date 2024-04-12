using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.StudentCRUD;
using Domain.StudentCRUD;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.StudentCRUD
{
    public class StudentService: IStudentService
    {
        public readonly ApplicationDBContext _dbContext;

        public StudentService(ApplicationDBContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<Student> AddStudent(Student student)
        {
            var result = await _dbContext.Students.AddAsync(student);
            await _dbContext.SaveChangesAsync();
            return result.Entity;
        }

        public async Task DeleteStudent(string id)
        {

            var student = await _dbContext.Students.Where(e => e.Id.ToString() == id).FirstOrDefaultAsync();
            if (student != null)
            {
                _dbContext.Students.Remove(student);
                await _dbContext.SaveChangesAsync();
                
            }
         
        }

        public async Task<IEnumerable<Student>> GetAllStudent()
        {
            var students = await _dbContext.Students.ToListAsync();
            return students;
        }

        public async Task<IEnumerable<Student>> GetStudentById(string id)
        {
            var result = await _dbContext.Students.Where(e => e.Id.ToString() == id).ToListAsync();

            return result;
        }

        public async Task<Student> UpdateStudent(Student student)
        {
            var init_student = await _dbContext.Students.FirstOrDefaultAsync(s => s.Id == student.Id);
            if (init_student != null)
            {
                init_student.Name = student.Name;
                init_student.Email = student.Email;
                init_student.Phone = student.Phone;
                init_student.Gender = student.Gender;
                await _dbContext.SaveChangesAsync();
                return init_student;
            }

            return null;
        }

      
    }
}
