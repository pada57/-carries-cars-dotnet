using Ardalis.GuardClauses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarriesCars.Domain.PricingEngine {

    public record CarRide(ReservationOptions ReservationOptions, RoadRecording RoadRecording);
    public record ReservationOptions(IDuration DurationToReachVehicle, Money PriceToExtendReservation);
    public record RoadRecording(IDuration Duration, IMileage Mileage, Money PricePerMinute);

    public class CarRideRecorder {
        private ReservationOptions _reservationOptions;
        private RoadRecording _roadRecording;
        private DateTime _reachTime;
        private Money _ridePrice;
        private Money _reservationPrice;
        private Currency _currency;
        private IDuration _freeReservationDuration;
        private readonly DateTime _reservationStartTime;

        private CarRideRecorder(Currency defaultCurrency, DateTime reservationStartTime) {
            _currency = defaultCurrency;
            _reservationStartTime = reservationStartTime;
        }

        public static CarRideRecorder Start(DateTime? startTime = null, Currency defaultCurrency = null) {

            //TODO move those defaults out
            return new CarRideRecorder(defaultCurrency ?? "EUR".Ccy(),
                startTime ?? DateTime.Now);
        }

        public CarRideRecorder WithReachCarTime(DateTime? reachTime = null) {
            _reachTime = reachTime ?? _reservationStartTime.AddMinutes(Constants.FreeReservationDuration.DurationInMinutes);

            return this;
        }

        public CarRideRecorder WithFreeReservationTimeInMinutes(IDuration freeReservationTimeInMinutes) {
            _freeReservationDuration = freeReservationTimeInMinutes;

            return this;
        }

        public CarRideRecorder WithRidePrice(Money ridePrice) {
            _ridePrice = ridePrice;
            _currency = ridePrice.Currency;

            return this;
        }

        public CarRideRecorder WithReservationPrice(Money reservationPrice) {
            _reservationPrice = reservationPrice;
            _currency = reservationPrice.Currency;

            return this;
        }

        public CarRideRecorder WithRoadRecording(IDuration durationInMinutes, int mileage) {
            _roadRecording = new RoadRecording(durationInMinutes, 
                new UnVerifiedMileage(mileage).Verify(),
                _ridePrice ?? new Money(Constants.DefaultRidePricePerMinute, _currency));

            return this;
        }

        public CarRide CheckOut() {
            Guard.Against.Null(_roadRecording, nameof(RoadRecording), "Cannot checkout if no road recording");

            _reservationOptions = new ReservationOptions(
                (new UnVerifiedDuration(Convert.ToInt32((_reachTime - _reservationStartTime).TotalMinutes)).Verify()
                    .DurationInMinutes - (_freeReservationDuration ?? Constants.FreeReservationDuration).DurationInMinutes).Duration(),
                _reservationPrice ?? new Money(Constants.DefaultReservationPricePerMinute, _currency));

            if (_reservationOptions.PriceToExtendReservation.Currency != _roadRecording.PricePerMinute.Currency)
                throw new ArgumentException(
                    $"Cannot checkout as different currency between reservation ({_reservationOptions.PriceToExtendReservation.Currency})" +
                    $" and road recording ({_roadRecording.PricePerMinute.Currency})");

            return new CarRide(_reservationOptions, _roadRecording);
        } 
    }
}
