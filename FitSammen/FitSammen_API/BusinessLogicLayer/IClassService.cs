namespace FitSammen_API.BusinessLogicLayer
{
    public interface IClassService
    {
        public IEnumerable<Model.Class> GetUpcomingClasses();
    }
}
