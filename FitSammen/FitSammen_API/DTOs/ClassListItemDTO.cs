using FitSammen_API.Model;
using System.Collections.Generic;

namespace FitSammen_API.DTOs
{
    public class ClassListItemDTO
    {
        public int ClassId { get; set; }
        public DateOnly Date { get; set; }
        public bool IsAvailable { get; set; }

        public string ClassName { get; set; }
        public string InstructorName { get; set; }
        public string Description { get; set; }
        public ClassType ClassType { get; set; }
        public TimeOnly StartTime { get; set; }
        public int DurationInMinutes { get; set; }
        public string RoomName { get; set; }
        public int Capacity { get; set; }
        public int ParticipantCount { get; set; }
        public int RemainingSpots { get; set; }
        public bool IsFull { get; set; }
    }
}
