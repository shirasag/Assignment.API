using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Assignment.API
{
    public class AssignmentsService: IAssignmentsService
    {
        private readonly AssignmentsContext _context;

        public AssignmentsService(AssignmentsContext context)
        {           
            _context = context;
        }

        public async Task<ActionResult<IEnumerable<Assignment>>> GetAll()
        {
            return await _context.Assignments.ToListAsync();           
        }

        public Assignment GetById(int id)
        {
            throw new NotImplementedException();
        }
    }

    public interface IAssignmentsService
    {
        Task<ActionResult<IEnumerable<Assignment>>> GetAll();
        Assignment GetById(int id);
    }
}
