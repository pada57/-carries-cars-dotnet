namespace CarriesCars.Domain.PricingEngine {
    public class PricingEngine : IPricingEngine {
        public Money CalculatePrice(CarRide carRide) {
            var (reservationOptions, roadRecording) = carRide;
            var ridePrice = Pricing(roadRecording.PricePerMinute, roadRecording.Duration);
            var reservationExtraPrice = Pricing(reservationOptions.PriceToExtendReservation, reservationOptions.DurationToReachVehicle);

            return ridePrice + reservationExtraPrice;
        }

        private Money Pricing(Money pricePerMinute, IDuration duration) {
            return pricePerMinute.MultiplyAndRound(duration.DurationInMinutes);
        }
    }

}
