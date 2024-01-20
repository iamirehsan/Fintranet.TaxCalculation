using Fintranet.TaxCalculation.Model.Entities.Base;
namespace Fintranet.TaxCalculation.Test.Test.Domain.Test;
public class VehicleTaxDateTest
{
    private Guid _vehicleId = Guid.NewGuid();
    private DateTime _time = DateTime.Now;
    private readonly VehicleTaxDate _vehicleTaxDate;

    public VehicleTaxDateTest()
    {
        _vehicleTaxDate = VehicleTaxDate.Create(_vehicleId, _time);
    }

    [Fact]
    public void Create_ValidParameters_ReturnsNonNullInstance()
    {
         // Assert
        Assert.NotNull(_vehicleTaxDate);
        Assert.Equal(_vehicleId, _vehicleTaxDate.VehicleId);
        Assert.Equal(_time, _vehicleTaxDate.Time);
    }
    [Fact]
    public void Create_ValidParameters_PropertiesAreCorrectlySet()
    {
      
        Assert.Equal(_vehicleId, _vehicleTaxDate.VehicleId);
        Assert.Equal(_time, _vehicleTaxDate.Time);
    }

    [Fact]
    public void TimeBaseOnHourAndMinuteAsString_FormattingIsCorrect()
    {
        // Arrange
   
        DateTime time = new DateTime(2023, 1, 1, 14, 30, 0);

        var vehicleTaxDate = VehicleTaxDate.Create(_vehicleId, time);

        // Act
        string formattedTime = vehicleTaxDate.TimeBaseOnHourAndMinuteAsString;

        // Assert
        Assert.Equal("14:30", formattedTime);
    }
}
