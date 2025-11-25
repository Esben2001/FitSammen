using FitSammenDekstopClient.Model;

namespace FitSammenDekstopClient.ServiceLayer
{
    public interface IClassService
    {
        Task<IEnumerable<Class>?> GetAllClassesAsync();
        Task<CreateClassResponse?> CreateClassAsync(CreateClassRequest request);
    }
}
