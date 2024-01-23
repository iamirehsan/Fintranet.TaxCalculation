using Fintranet.TaxCalculation.Model.Entities.Base;
using System;

public class Vehicle : BaseEntity
{
    public string? Name { get; private set; }
    public Guid? VehicleTypeId { get; private set; }
    public virtual VehicleType? VehicleType { get; private set; }

    private Vehicle(string? name, Guid? vehicleTypeId) : base()
    {
        Name = name;
        VehicleTypeId = vehicleTypeId;
    }
   
    public static Vehicle Create(string? name, Guid? vehicleTypeId = null) {  return new Vehicle(name, vehicleTypeId);}
}