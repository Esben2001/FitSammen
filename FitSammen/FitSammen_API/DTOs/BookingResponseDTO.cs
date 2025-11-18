namespace FitSammen_API.DTOs
{
    public class BookingResponseDTO
    {
        public int BookingId { get; set; }
        public string Status { get; set; } = string.Empty;
        public string Message { get; set; } = string.Empty;
    }
}
