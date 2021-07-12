using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MyApp.Data;
using MyApp.Dtos;
using MyApp.Interfaces;
using MyApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentController : ControllerBase
    {
        private readonly IStudentRepository _studentRepository;
        private readonly MyContext _context;

        public StudentController(IStudentRepository studentRepository , MyContext context)
        {
            _studentRepository = studentRepository;
            _context = context;
        }
        #region GetALLSTUDENT

        [HttpGet]
        [Route("")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        public async Task<IActionResult> GetAllStudent()
        {
            var studentList =  await _studentRepository.GetAll();

            return Ok(studentList.Select(x => new { 
                    x.StudentId,
                    x.StudentName,
                    x.Email,
                    x.Address,
                    x.PhoneNumber 
            }));
        }

        [HttpGet]
        [Route("{id}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]

        public async Task<IActionResult> GetById(int id)
        {
            var result = await _studentRepository.GetStudentById(id);

            return Ok(new{
                   result.StudentId,
                   result.StudentName,
                   result.Email,
                   result.Address,
                   result.PhoneNumber
            });
        }

        #endregion

        #region ADD STUDENT

        [HttpPost]
        [Route("add")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]

        public async Task<IActionResult> AddStudentData([FromBody]StudentDto studentDto)
        {
            var studObj = new Student()
            {
                StudentName = studentDto.StudentName,
                Email = studentDto.Email,
                Address = studentDto.Address,
                PhoneNumber = studentDto.PhoneNumber
            };

            await _studentRepository.AddStudent(studObj);

            await _studentRepository.SaveChanges();

            return Ok(new
            {
                studObj.StudentId,
                studObj.StudentName,
                studObj.Email,
                studObj.Address,
                studObj.PhoneNumber
            });
        }

        #endregion

        #region UPDATE_STUDENT
        [HttpPut]
        [Route("update/{id}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]

        public async Task<IActionResult> UpdateStudent(int id ,[FromBody] StudentDto studentDto)
        {

            var stud = await _studentRepository.GetStudentById(id);

            if(stud == null)
            {
                return NotFound();
            }

            stud.StudentName = studentDto.StudentName;
            stud.Email = studentDto.Email;
            stud.Address = studentDto.Address;
            stud.PhoneNumber = studentDto.PhoneNumber;

            await  _studentRepository.SaveChanges();

            return Ok(new { 
                stud.StudentId,
                stud.StudentName,
                stud.Email,
                stud.Address,
                stud.PhoneNumber
            });
        }

        #endregion
    }
}
