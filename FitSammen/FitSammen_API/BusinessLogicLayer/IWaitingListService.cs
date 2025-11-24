namespace FitSammen_API.BusinessLogicLayer
{
    public interface IWaitingListService
    {
        public WaitingListResult AddMemberToWaitingList(int classId, int memberId);
    }
}
