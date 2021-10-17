using Ardalis.GuardClauses;
using CSharpFunctionalExtensions;
using System.Collections.Generic;

namespace CarriesCars.Domain.PricingEngine
{
    public interface IMileage {
        int MileageInKilometers { get; }
    }

    public class UnVerifiedMileage : ValueObject, IMileage
    {
        private record VerifiedMileage(int MileageInKilometers) : IMileage;
        
        public UnVerifiedMileage(int mileageInKilometers)
        {
            MileageInKilometers = mileageInKilometers;
        }

        public int MileageInKilometers { get; }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return MileageInKilometers;
        }

        public IMileage Verify()
        {
            Guard.Against.NegativeOrZero(MileageInKilometers, nameof(MileageInKilometers), "Mileage should be a positive number");

            return new VerifiedMileage(MileageInKilometers);
        }
    }
}
