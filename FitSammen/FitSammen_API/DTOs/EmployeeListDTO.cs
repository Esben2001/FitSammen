namespace FitSammen_API.DTOs
{
    public class EmployeeListDTO
    {
        public int User_ID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }

        public EmployeeListDTO(int employeeId, string firstName, string lastName)
        {
            User_ID = employeeId;
            FirstName = firstName;
            LastName = lastName;
        }
    }
}
