using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Assignment.API
{
    public class AssignmentsService
    {
        private readonly AssignmentsContext _context;

        public AssignmentsService(AssignmentsContext context)
        {           
            _context = context;
        }

        public IEnumerable<Assignment> GetAll()
        {
            List<Assignment> ass = new List<Assignment>();
            return ass;
        }

    }

    public interface IAssignmentsService
    {        
        IEnumerable<Assignment> GetAll();
        Assignment GetById(int id);
    }
}
