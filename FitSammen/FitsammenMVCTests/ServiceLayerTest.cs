//using FitSammenWebClient.Models;
//using FitSammenWebClient.ServiceLayer;
//using Microsoft.Extensions.Configuration;

//namespace FitsammenMVCTests
//{
//    public class ServiceLayerTest
//    {
//        private readonly ClassService _service;

//        public ServiceLayerTest()
//        {
//            var settings = new Dictionary<string, string?>
//            {
//                { "ServiceUrlToUse", "https://localhost:7229/api/" }
//            };

//            IConfiguration config = new ConfigurationBuilder()
//                .AddInMemoryCollection(settings)
//                .Build();

//            _service = new ClassService(config);
//        }

//        [Fact]
//        public async Task GetAllClassesFromApiReturnsList_ReturnListOfClases()
//        {
//            //Act
//            IEnumerable<Class>? result = await _service.GetClasses();

//            // Assert
//            Assert.NotNull(result);
//            Assert.NotEmpty(result);
//        }
//    }
//}
