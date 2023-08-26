using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication1.Models
{
    public class ABS
    {
        public int studentId { get; set; }
        public string studentName { get; set; }
        public string Gmail { get; set; }
        public string DateOfJoining { get; set; }
    }
}
