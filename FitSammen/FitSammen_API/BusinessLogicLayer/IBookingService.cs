namespace FitSammen_API.BusinessLogicLayer
{
    public interface IBookingService
    {
        public BookingResult BookClass(int memberId, int classId);
    }
}
