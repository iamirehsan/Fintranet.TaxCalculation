using System.ComponentModel.DataAnnotations.Schema;

namespace Fintranet.TaxCalculation.Model.Entities.Base
{
    public class VehicleTaxDate : BaseEntity
    {
        public Guid VehicleId { get; private set; }
        public DateTime Time { get; private set; }
        public string TimeBaseOnHourAndMinuteAsString => $"{Time.Hour}:{Time.Minute}";

        private VehicleTaxDate(Guid vehicleId, DateTime time) : base()
        {
            VehicleId = vehicleId;
            Time = time;
        }
        public static VehicleTaxDate Create(Guid vehicleId, DateTime time) { return new VehicleTaxDate(vehicleId, time); }
    }
}
