using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FitSammenDekstopClient.Model
{
    public class CreateClassResponse
    {
        public int ClassId { get; set; }
        public Class? CreatedClass { get; set; }
        public CreateClassStatus Status { get; set; }
        public string? Message { get; set; }
    }

    public enum CreateClassStatus
    {
        Success,
        Error
    }
}
