using Fintranet.TaxCalculation.Model.Entities.Base;
namespace Fintranet.TaxCalculation.Test.Test.Domain.Test;
public class VehicleTypeTest
{
    [Fact]
    public void CreateVehicleType_ValidParameters_InstanceIsCreatedCorrectly()
    {
        // Arrange
        string name = "TestVehicleType";
        bool isTaxFree = true;

        // Act
        VehicleType vehicleType = VehicleType.Create(name, isTaxFree);

        // Assert
        Assert.NotNull(vehicleType);
        Assert.Equal(name, vehicleType.Name);
        Assert.Equal(isTaxFree, vehicleType.IsTaxFree);
        Assert.Null(vehicleType.Vehicles); // Assuming Vehicles is initially null


    }
}
