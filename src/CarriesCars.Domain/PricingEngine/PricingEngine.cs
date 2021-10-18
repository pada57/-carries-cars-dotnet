namespace CarriesCars.Domain.PricingEngine {
    public class PricingEngine : IPricingEngine {
        public Money CalculatePrice(CarRide carRide) {
            var (reservationOptions, roadRecording) = carRide;
            var ridePrice = MultiplyAndRound(roadRecording.PricePerMinute, roadRecording.Duration);
            var reservationExtraPrice = MultiplyAndRound(reservationOptions.PriceToExtendReservation, reservationOptions.DurationToReachVehicle);
            var mileageChargePrice = roadRecording.ChargePerKilometer.MultiplyAndRound(roadRecording.MileageWithExtraCharges);

            return ridePrice + reservationExtraPrice + mileageChargePrice;
        }

        private Money MultiplyAndRound(Money pricePerMinute, IDuration duration) {
            return pricePerMinute.MultiplyAndRound(duration.DurationInMinutes);
        }
    }

}
