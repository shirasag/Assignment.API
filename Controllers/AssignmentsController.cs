using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Assignment.API;

namespace Assignment.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AssignmentsController : ControllerBase
    {
        private readonly AssignmentsContext _context;
       

        public AssignmentsController(AssignmentsContext context)
        {
            _context = context;            
        }

     
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Assignment>>> GetAssignments()
        {
            List<Assignment> list = await _context.Assignments.ToListAsync();
            // ((endDate != null) ? ((endDate-startDate).Value.TotalDays > 7 ? true : isArchived) : isArchived);
            foreach (var item in list)
            {
                item.isArchived = (item.endDate != null) ? ((DateTime.Now - item.endDate).Value.TotalDays > 7 ? true : item.isArchived) : item.isArchived;
            }
            return list;
        }
      
        [HttpGet("{id}")]
        public async Task<ActionResult<Assignment>> GetAssignment(int id)
        {
          if (_context.Assignments == null)
          {
              return NotFound();
          }
            var assignment = await _context.Assignments.FindAsync(id);

            if (assignment == null)
            {
                return NotFound();
            }

            return assignment;
        }

        

        [HttpPut("Archive/{id}")]
        public async Task<IActionResult> Archive(int id)
        {
            var assignment = await _context.Assignments.FindAsync(id);

            if (assignment == null)
            {
                return NotFound();
            }
            _context.Entry(assignment).Entity.isArchived = true;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                throw;
            }

            return NoContent();
        }


        [HttpPut("End/{id}")]
        public async Task<IActionResult> End(int id)
        {
            var assignment = await _context.Assignments.FindAsync(id);

            if(assignment == null)
            {
                return NotFound();
            }
            _context.Entry(assignment).Entity.isEnded = true;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                throw;
            }

            return NoContent();
        }
         
        [HttpPost]
        public async Task<ActionResult<Assignment>> PostAssignment(Assignment assignment)
        {        
            
            if (_context.Assignments == null)
          {
              return Problem("Entity set 'AssignmentsContext.Assignments'  is null.");
          }
            _context.Assignments.Add(assignment);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetAssignment", new { id = assignment.id }, assignment);
        }
     
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAssignment(int id)
        {
            if (_context.Assignments == null)
            {
                return NotFound();
            }
            var assignment = await _context.Assignments.FindAsync(id);
            if (assignment == null)
            {
                return NotFound();
            }

            _context.Assignments.Remove(assignment);
            await _context.SaveChangesAsync();

            return NoContent();
        }
       
    }
}
