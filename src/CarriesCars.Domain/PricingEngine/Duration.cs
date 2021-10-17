using Ardalis.GuardClauses;
using CSharpFunctionalExtensions;
using System.Collections.Generic;

namespace CarriesCars.Domain.PricingEngine
{
    public interface IDuration {
        int DurationInMinutes { get; }
    }

    public class UnVerifiedDuration : ValueObject, IDuration
    {
        private record VerifiedDuration(int durationInMinutes) : IDuration
        {
            public int DurationInMinutes => durationInMinutes;
        }

        private readonly int durationInMinutes;

        public UnVerifiedDuration(int durationInMinutes)
        {
            this.durationInMinutes = durationInMinutes;
        }

        public int DurationInMinutes => durationInMinutes;

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return durationInMinutes;
        }

        public IDuration Verify()
        {
            Guard.Against.Negative(durationInMinutes, nameof(durationInMinutes), "Duration should be a positive number in minutes");

            return new VerifiedDuration(durationInMinutes);
        }        
    }

    public static class DurationExtensions {
        public static IDuration Duration(this int minutes) {
            return new UnVerifiedDuration(minutes).Verify();
        }
    }
}
