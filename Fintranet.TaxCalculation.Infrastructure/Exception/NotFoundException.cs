﻿namespace Fintranet.TaxCalculation.Infrastructure.Exception
{
    [Serializable]
    public class NotFoundException : System.Exception
    {
        public NotFoundException()
        {
        }

        public NotFoundException(string message) : base(message)
        {
        }
    }
}
