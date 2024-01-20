using Fintranet.TaxCalculation.Model.Entities.Base;

namespace Fintranet.TaxCalculation.Test.Test.Domain.Test
{
    public class CityTest
    {
        private readonly City _city;
        private readonly string cityName = "Tehran";

        public CityTest()
        {
            _city = City.Create(cityName);
         
        }

        [Fact]
        public void Create_ValidParameters_ReturnsNonNullInstance()
        {
         
            Assert.NotNull(_city);

        }
        [Fact]
        public void Create_WithValidParameters_SetsNameCorrectly()
        {
                        
            Assert.Equal(cityName, _city.Name);
        }
    }
}
