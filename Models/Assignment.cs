using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Assignment.API;


namespace Assignment.API
{
    [Table("Assignments")]
    public class Assignment
    {        
        public int id { get; set; }
        
        [Required]
        public AssignmentType type { get; set; }
        
        [Required]
        public string name { get; set; }

        public string? desc { get; set; }
        
        [Required]
        public DateTime startDate { get; set; }
        
        public DateTime? endDate { get; set; }

        [Required] 
        public bool isRepeated { get; set; }
        
        public bool isEnded { get; set; }
        public bool? isArchived { get; set; }
        [NotMapped]
        public Status status => (Status)(isEnded ? 1 : 0);
	
    }

    public enum AssignmentType
    {
        Personal,
        Work,
        Study
    }
   
    public enum Status
    {
        Active,
        Done
    }

}
