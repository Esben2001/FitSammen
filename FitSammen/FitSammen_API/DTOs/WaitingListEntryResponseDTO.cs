using FitSammen_API.BusinessLogicLayer;

namespace FitSammen_API.DTOs
{
    public class WaitingListEntryResponseDTO
    {
        public int WaitingListEntryPosition { get; set; }
        public WaitingListStatus Status { get; set; }
        public string Message { get; set; } = string.Empty;
    }
}
