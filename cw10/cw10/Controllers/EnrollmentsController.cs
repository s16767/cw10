using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using cw10.Models;
using cw10.DTOs;

namespace cw10.Controllers
{
    [Route("api/enrollstudent")]
    [ApiController]
    public class EnrollmentsController : ControllerBase
    {
        private readonly s16767Context _context;

        public EnrollmentsController(s16767Context context)
        {
            _context = context;
        }

 
        [HttpPost]
        public async Task<ActionResult> EnrollStudent(EnrollStudentRequest student)
        {
            var study = await _context.Studies.FirstOrDefaultAsync(s => s.Name.Equals(student.Studies));

            if (study == null)
            {
                return NotFound("Nie znaleziono takich studiów");
            }

            var enrollment = await _context.Enrollment.FirstOrDefaultAsync(e => e.IdStudy == study.IdStudy && e.Semester == 1);

            if(enrollment == null)
            {
                var enroll = new Enrollment
                {
                    IdEnrollment = await _context.Enrollment.MaxAsync(e => e.IdEnrollment) + 1,
                    Semester = 1,
                    IdStudy = study.IdStudy,
                    StartDate = DateTime.Now
                };

                _context.Enrollment.Add(enroll);
                _context.SaveChanges();
                enrollment = enroll;
            }

            if(_context.Student.Any(s => s.IndexNumber.Equals(student.IndexNumber)))
            {
                return BadRequest("Student z podanym indexem juz istnieje");
            }

            var st = new Student
            {
                IndexNumber = student.IndexNumber,
                FirstName = student.FirstName,
                LastName = student.LastName,
                BirthDate = student.BirthDate,
                IdEnrollment = enrollment.IdEnrollment
            };

            _context.Student.Add(st);
            _context.SaveChanges();


            return Ok("Dodano studenta");
        }

       // [Route("/promotion")]
        [HttpPost("/promotion")]
        public async Task<ActionResult> PromoteStudent(PromoteStudentRequest promote)
        {
            var study = await _context.Studies.FirstOrDefaultAsync(s => s.Name.Equals(promote.Studies));

            if(study == null)
            {
                return NotFound("Nie znaleziono takich studiów");
            }

            var enrollment = await _context.Enrollment.FirstOrDefaultAsync(e => e.IdStudy == study.IdStudy);

            if(enrollment == null)
            {
                return NotFound("Nie znaleziono takiego wpisu");
            }


            return Ok();
        }

    }
}
