using Fintranet.TaxCalculation.Infrastructure.Enum;
using Fintranet.TaxCalculation.Message.Commands;
using Fintranet.TaxCalculation.Model.Entities.Base;
using System.Collections;

namespace Fintranet.TaxCalculation.Test.TestData
{
    public class TaxCalculatorCalculateCommandTestData : IEnumerable<object[]>
    {

        public IEnumerator<object[]> GetEnumerator()
        {

            yield return new object[]
            { new CalculateCommand(InitialTaxCalculatorData.VehicleId, InitialTaxCalculatorData.CityId, 1402) };


        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }

    
 
}
