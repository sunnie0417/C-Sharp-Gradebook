using GradeBook.Data;
using GradeBook.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace GradeBook.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AssignmentsController : ControllerBase
    {
        private readonly AppDbContext _context;

        public AssignmentsController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Assignment>>> GetAll()
        {
            return await _context.Assignments.ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Assignment>> Get(int id)
        {
            var assignment = await _context.Assignments.FindAsync(id);
            return assignment == null ? NotFound() : assignment;
        }

        [HttpGet("{id}/grades")]
        public async Task<ActionResult<IEnumerable<object>>> GetGradesForAssignment(int id)
        {
            var grades = await _context.Grades
                .Where(g => g.AssignmentId == id)
                .Select(g => new
                {
                    g.Id,
                    g.Score,
                    Student = new
                    {
                        g.Student.Id,
                        g.Student.FirstName,
                        g.Student.LastName
                    }
                })
                .ToListAsync();

            return grades;
        }

        [HttpPost]
        public async Task<ActionResult<Assignment>> Create(Assignment assignment)
        {
            _context.Assignments.Add(assignment);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(Get), new { id = assignment.Id }, assignment);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, Assignment assignment)
        {
            if (id != assignment.Id) return BadRequest();
            _context.Entry(assignment).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var assignment = await _context.Assignments.FindAsync(id);
            if (assignment == null) return NotFound();
            _context.Assignments.Remove(assignment);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}