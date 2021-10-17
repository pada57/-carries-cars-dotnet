using System;

namespace CarriesCars.Domain.PricingEngine
{
    public class PricingEngine : IPricingEngine<Money>
    {
        public Money CalculatePrice(Money pricePerMinute, IDuration duration, Money reservationExtraPricePerMinute = null, IDuration reservationDuration = null) {
            if (duration is UnVerifiedDuration) throw new ArgumentException("Duration must be verified");
            if (duration is UnVerifiedDuration) throw new ArgumentException("Duration must be verified");

            var ridePrice = Pricing(pricePerMinute, duration);

            if (reservationExtraPricePerMinute is null) return ridePrice;

            var reservationExtraPrice = Pricing(reservationExtraPricePerMinute, reservationDuration);

            return ridePrice + reservationExtraPrice;
        }

        public IDuration DurationInMinutes(int minutes)
        {
            return new UnVerifiedDuration(minutes).Verify();
        }

        private Money Pricing(Money pricePerMinute, IDuration duration) {
            return pricePerMinute.MultiplyAndRound(duration.DurationInMinutes);
        }
    }

}
