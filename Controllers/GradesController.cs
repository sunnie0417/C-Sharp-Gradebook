using GradeBook.Data;
using GradeBook.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace GradeBook.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GradesController : ControllerBase
    {
        private readonly AppDbContext _context;

        public GradesController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<object>>> GetAll()
        {
            return await _context.Grades
                .Select(g => new
                {
                    g.Id,
                    g.Score,
                    Student = new
                    {
                        g.Student.Id,
                        g.Student.FirstName,
                        g.Student.LastName
                    },
                    Assignment = new
                    {
                        g.Assignment.Id,
                        g.Assignment.Title
                    }
                })
                .ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<object>> Get(int id)
        {
            var grade = await _context.Grades
                .Where(g => g.Id == id)
                .Select(g => new
                {
                    g.Id,
                    g.Score,
                    Student = new
                    {
                        g.Student.Id,
                        g.Student.FirstName,
                        g.Student.LastName
                    },
                    Assignment = new
                    {
                        g.Assignment.Id,
                        g.Assignment.Title
                    }
                })
                .FirstOrDefaultAsync();

            return grade == null ? NotFound() : grade;
        }

        [HttpPost]
        public async Task<ActionResult<Grade>> Create(Grade grade)
        {
            _context.Grades.Add(grade);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(Get), new { id = grade.Id }, grade);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, Grade grade)
        {
            if (id != grade.Id) return BadRequest();
            _context.Entry(grade).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var grade = await _context.Grades.FindAsync(id);
            if (grade == null) return NotFound();
            _context.Grades.Remove(grade);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}
