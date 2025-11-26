using System.ComponentModel.DataAnnotations;

namespace FitSammen_API.DTOs
{
    public class EmployeeMinimalDTO
    {
        [Range(1, int.MaxValue, ErrorMessage = "EmployeeId must be a positive integer.")]
        public int User_ID { get; set; }
        public EmployeeMinimalDTO(int employeeId)
        {
            User_ID = employeeId;
        }
    }
}
