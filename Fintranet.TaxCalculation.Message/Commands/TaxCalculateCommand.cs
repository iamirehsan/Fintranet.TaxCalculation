using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fintranet.TaxCalculation.Message.Commands
{
    public record CalculateCommand(Guid vehicleId, Guid cityId, int year);
   
}
