namespace Fintranet.TaxCalculation.Test.Test.Domain.Test;
public class VehicleTest
{
    [Fact]
    public void CreateVehicle_ValidParameters_ReturnsVehicleInstance()
    {
        // Arrange
        string name = "TestVehicle";
        Guid vehicleTypeId = Guid.NewGuid();

        // Act
        Vehicle vehicle = Vehicle.Create(name, vehicleTypeId);

        // Assert
        Assert.NotNull(vehicle);
        Assert.Equal(name, vehicle.Name);
        Assert.Equal(vehicleTypeId, vehicle.VehicleTypeId);
        Assert.Null(vehicle.VehicleType); // Assuming VehicleType is initially null


    }
}
