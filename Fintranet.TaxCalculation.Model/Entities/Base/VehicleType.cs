namespace Fintranet.TaxCalculation.Model.Entities.Base
{
    public class VehicleType : BaseEntity
    {
        public string? Name { get; private set; }
        public bool IsTaxFree { get; private set; }
        public virtual ICollection<Vehicle>? Vehicles { get; private set; }

        private VehicleType(string? name, bool isTaxFree) : base()
        {
            Name = name;
            IsTaxFree = isTaxFree;
          
        }

        public static VehicleType Create(string? name, bool isTaxFree) { return new VehicleType(name,isTaxFree); }
    }
}
