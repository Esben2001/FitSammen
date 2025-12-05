namespace FitSammen_API.BusinessLogicLayer
{
    public class WaitingListResult
    {
        public WaitingListStatus Status { get; set; }
        public int? WaitingListPosition { get; set; }
    }
    public enum WaitingListStatus
    {
        Success,
        AlreadySignedUpWL,
        AlreadySignedUpMB,
        Error
    }
}
