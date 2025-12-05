using FitSammen_API.DatabaseAccessLayer;
using FitSammen_API.Exceptions;

namespace FitSammen_API.BusinessLogicLayer
{
    public class WaitingListService : IWaitingListService
    {
        private readonly IMemberAccess _memberAccess;

        public WaitingListService(IMemberAccess memberAccess)
        {
            _memberAccess = memberAccess;
        }
        public WaitingListResult AddMemberToWaitingList(int classId, int memberId)
        {
            WaitingListResult result = new WaitingListResult();
            try
            {
                if(_memberAccess.IsMemberSignedUp(memberId, classId))
                {
                    result = new WaitingListResult
                    {
                        Status = WaitingListStatus.AlreadySignedUpMB,
                        WaitingListPosition = null
                    };
                    return result;
                }
                int posRes = _memberAccess.IsMemberOnWaitingList(memberId, classId);
                if (posRes > 0)
                {
                    result = new WaitingListResult
                    {
                        Status = WaitingListStatus.AlreadySignedUpWL,
                        WaitingListPosition = posRes
                    };
                }
                else
                {
                    int position = _memberAccess.CreateWaitingListEntry(memberId, classId);
                    if (position > 0)
                    {
                        result = new WaitingListResult
                        {
                            Status = WaitingListStatus.Success,
                            WaitingListPosition = position
                        };
                    }
                    else
                    {
                        result = new WaitingListResult
                        {
                            Status = WaitingListStatus.Error,
                            WaitingListPosition = null
                        };
                    }
                }
            }
            catch (DataAccessException)
            {
                result = new WaitingListResult
                {
                    Status = WaitingListStatus.Error,
                    WaitingListPosition = null
                };
            }
            return result;
        }
    }
}

