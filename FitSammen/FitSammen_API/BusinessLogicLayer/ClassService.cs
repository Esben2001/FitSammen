using FitSammen_API.DatabaseAccessLayer;
using FitSammen_API.Exceptions;

namespace FitSammen_API.BusinessLogicLayer
{
    public class ClassService : IClassService
    {
        private readonly IClassAccess _classAccess;

        public ClassService(IClassAccess classAccess)
        {
            _classAccess = classAccess;
        }

        public IEnumerable<Model.Class> GetUpcomingClasses()
        {
            try
            {
                return _classAccess.GetUpcomingClasses();
            }
            catch (DataAccessException)
            {
                throw;
            }

        }
    }
}
