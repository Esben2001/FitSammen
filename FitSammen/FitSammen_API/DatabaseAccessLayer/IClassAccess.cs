using FitSammen_API.Model;

namespace FitSammen_API.DatabaseAccessLayer
{
    public interface IClassAccess
    {
        public IEnumerable<Class> GetAllClasses(ClassType classType, Location location, DateOnly endDate);
    }
}
