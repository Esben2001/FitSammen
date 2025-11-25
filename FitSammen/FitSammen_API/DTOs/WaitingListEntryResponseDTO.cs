using FitSammen_API.BusinessLogicLayer;

namespace FitSammen_API.DTOs
{
    public class WaitingListEntryResponseDTO
    {
        public int WaitingListPosition { get; set; }
        public WaitingListStatus Status { get; set; }
        public string Message { get; set; } = string.Empty;
    }
}
