namespace FitSammen_API.DTOs
{
    public class RoomListDTO
    {
        public int? RoomId { get; set; }
        public string RoomName { get; set; }
        public int? Capacity { get; set; }
        public LocationMinimalDTO Location { get; set; }

        public RoomListDTO(int? roomId, string roomName, int? capacity, LocationMinimalDTO locationDTO)
        {
            RoomId = roomId;
            RoomName = roomName;
            Capacity = capacity;
            Location = locationDTO;
        }
    }
}
