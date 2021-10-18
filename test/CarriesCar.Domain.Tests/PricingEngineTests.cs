using CarriesCars.Domain;
using CarriesCars.Domain.PricingEngine;
using FluentAssertions;
using System;
using Xunit;

namespace CarriesCar.Domain.Tests {
    public class GivenAPricingEngine {
        protected PricingEngine _pricingEngine;

        public GivenAPricingEngine() {
            _pricingEngine = new PricingEngine();
        }

        public class WhenCalculatePrice : GivenAPricingEngine {


            public WhenCalculatePrice() {
            }

            [Fact]
            public void With_Price_Per_Minute_Then_should_return_standard_pricing() {

                var carRideRecorder = CarRideRecorder.Start()
                    .WithReachCarTime()
                    .WithRidePrice(30.EUR())
                    .WithRoadRecording(1.Duration(), 100)
                    .CheckOut();                

                var calculatedPrice = _pricingEngine.CalculatePrice(carRideRecorder);

                30.EUR().Should().Be(calculatedPrice);
            }

            [Fact]
            public void With_Extra_Reservation_Not_Exceeding_1EUR_Then_should_return_standard_pricing() {
                var startTime = DateTime.Now;
                var reachTime = startTime.AddMinutes(25);

                var carRideRecorder = CarRideRecorder.Start()
                    .WithReachCarTime(reachTime)
                    .WithReservationPrice(0.09.EUR())
                    .WithRidePrice(30.EUR())
                    .WithRoadRecording(1.Duration(), 100)                    
                    .CheckOut();

                var calculatedPrice = _pricingEngine.CalculatePrice(carRideRecorder);

                30.EUR().Should().Be(calculatedPrice);
            }

            [Fact]
            public void With_Extra_Reservation_Exceeding_1EUR_Then_should_return_standard_pricing_plus_extra_reservation_price() {
                var startTime = DateTime.Now;
                var reachTime = startTime.AddMinutes(31);

                var carRideRecorder = CarRideRecorder.Start()
                    .WithReachCarTime(reachTime)
                    .WithReservationPrice(0.09.EUR())
                    .WithRidePrice(30.EUR())
                    .WithRoadRecording(1.Duration(), 100)
                    .CheckOut();

                var calculatedPrice = _pricingEngine.CalculatePrice(carRideRecorder);

                (30.EUR() + 1.EUR()).Should().Be(calculatedPrice);
            }

            [Fact]
            public void With_ExtraMileage_Then_should_return_pricing_with_extra_charges() {

                var carRideRecorder = CarRideRecorder.Start()
                    .WithReachCarTime()
                    .WithRidePrice(30.EUR())
                    .WithRoadRecording(1.Duration(), 300)
                    .CheckOut();

                var calculatedPrice = _pricingEngine.CalculatePrice(carRideRecorder);

                var expected = 30 + Convert.ToInt32(50 * Constants.DefaultChargePricePerKilometer);

                expected.EUR().Should().Be(calculatedPrice);
            }

            [Fact]
            public void With_Extra_Reservation_Exceeding_1EUR_And_ExtraMileage_Then_should_return_standard_pricing_plus_extra_reservation_price_plus_mileage_charges() {
                var startTime = DateTime.Now;
                var reachTime = startTime.AddMinutes(31);

                var carRideRecorder = CarRideRecorder.Start()
                    .WithReachCarTime(reachTime)
                    .WithReservationPrice(0.09.EUR())
                    .WithRidePrice(30.EUR())
                    .WithRoadRecording(1.Duration(), 300)
                    .CheckOut();

                var calculatedPrice = _pricingEngine.CalculatePrice(carRideRecorder);

                (30.EUR() + 1.EUR() + 10.EUR()).Should().Be(calculatedPrice);
            }
        }
    }
}

