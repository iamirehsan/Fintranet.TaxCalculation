namespace Fintranet.TaxCalculation.Model.Entities.Base
{
    public class City : BaseEntity
    {
        public string? Name { get; private set; }
        public virtual InitialSetting? InitialSetting { get; private set; }

        private City(string? name) : base()
        {
            Name = name;
        }
        public static City Create(string? name) { return new City(name); }
    }

}

