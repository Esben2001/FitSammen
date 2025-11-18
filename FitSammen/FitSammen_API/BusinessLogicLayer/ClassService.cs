using FitSammen_API.DatabaseAccessLayer;

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
            return _classAccess.GetUpcomingClasses();
        }
    }
}
