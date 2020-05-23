using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using cw10.Models;

namespace cw10.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentsController : ControllerBase
    {
        private readonly s16767Context _context;

        public StudentsController(s16767Context context)
        {
            _context = context;
        }

        // GET: api/Students
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Student>>> GetStudent()
        {
            return await _context.Student.ToListAsync();
        }

        [HttpPost]
        public async Task<ActionResult<Student>> UpdateStudent(Student student)
        {
            string resp;

            var s = _context.Student.SingleOrDefault(s => s.IndexNumber == student.IndexNumber);

            if (s != null)
            {
                _context.Entry(s).CurrentValues.SetValues(student);
                await _context.SaveChangesAsync();
                resp = "Zaktualizowano studenta";
            }
            else
            {
                resp = "Nie zaktualizowano studenta";
            }

            return Ok(resp);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<Student>> DeleteStudent(string id)
        {
            var student = await _context.Student.FindAsync(id);
            if (student == null)
            {
                return NotFound();
            }

            _context.Student.Remove(student);
            await _context.SaveChangesAsync();

            return student;
        }

    }

}
