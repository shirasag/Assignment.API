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
        private readonly IAssignmentsService _assignmentService;

        public AssignmentsController(AssignmentsContext context, IAssignmentsService assignmentService)
        {
            _context = context;
            _assignmentService = assignmentService;
        }

     
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Assignment>>> GetAssignments()
        {
            return await _context.Assignments.ToListAsync();
            //ActionResult<IEnumerable<Assignment>> assignmentList = _assignmentService.GetAll();
            //if (assignmentList == null)
            //{
            //    return NotFound();
            //}
            //return assignmentList;
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

        // PUT: api/Assignments/5      
        [HttpPut("{id}")]
        public async Task<IActionResult> PutAssignment(int id, Assignment assignment)
        {
            if (id != assignment.id)
            {
                return BadRequest();
            }

            _context.Entry(assignment).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AssignmentExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        [HttpPost("Archive/{id}")]
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
                if (!AssignmentExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }


        [HttpPost("End/{id}")]
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
                if (!AssignmentExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
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

        private bool AssignmentExists(int id)
        {
            return (_context.Assignments?.Any(e => e.id == id)).GetValueOrDefault();
        }
    }
}
