using FitSammen_API.DatabaseAccessLayer;
using FitSammen_API.Exceptions;

namespace FitSammen_API.BusinessLogicLayer
{
    public class BookingService : IBookingService
    {
        private readonly IMemberAccess _memberAccess;

        public BookingService(IMemberAccess memberAccess)
        {
            _memberAccess = memberAccess;
        }

        public BookingResult BookClass(int memberId, int classId)
        {
            try
            {
                int bookingId = _memberAccess.CreateMemberBooking(memberId, classId);

                return new BookingResult
                {
                    Status = BookingStatus.Success,
                    BookingID = bookingId
                };
            }
            catch (DataAccessException)
            {
                return new BookingResult
                {
                    Status = BookingStatus.ClassFull,
                    BookingID = 0
                };
            }
            catch (Exception)
            {
                return new BookingResult
                {
                    Status = BookingStatus.Error,
                    BookingID = 0
                };
            }
        }
    }
}
