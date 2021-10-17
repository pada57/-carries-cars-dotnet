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
                var pricePerMinute = 30.EUR();
                var duration = _pricingEngine.DurationInMinutes(1);

                var calculatedPrice = _pricingEngine.CalculatePrice(pricePerMinute, duration);

                30.EUR().Should().Be(calculatedPrice);
            }

            [Fact]
            public void With_Extra_Reservation_Not_Exceeding_1EUR_Then_should_return_standard_pricing() {
                var pricePerMinute = 30.EUR();
                var duration = _pricingEngine.DurationInMinutes(1);

                var extraPricePerMinute = 0.09.EUR();
                var extraReservation = _pricingEngine.DurationInMinutes(5);

                var calculatedPrice = _pricingEngine.CalculatePrice(pricePerMinute, duration, extraPricePerMinute, extraReservation);

                30.EUR().Should().Be(calculatedPrice);
            }

            [Fact]
            public void With_Extra_Reservation_Not_Exceeding_1EUR_Then_should_return_standard_pricing_plus_extra_reservation_price() {
                var pricePerMinute = 30.EUR();
                var duration = _pricingEngine.DurationInMinutes(1);

                var extraPricePerMinute = 0.09.EUR();
                var extraReservation = _pricingEngine.DurationInMinutes(11);

                var calculatedPrice = _pricingEngine.CalculatePrice(pricePerMinute, duration, extraPricePerMinute, extraReservation);

                (30.EUR() + 1.EUR()).Should().Be(calculatedPrice);
            }
        }

        public class WhenDurationInMinutes : GivenAPricingEngine {

            [Fact]
            public void Guards_Against_0_Or_Negative_Duration() {
                Action ZeroPricingDurationPassed = () => _pricingEngine.DurationInMinutes(0);

                Assert.Throws<ArgumentException>(ZeroPricingDurationPassed);
            }

            [Fact]
            public void Duration_Verifies_Valid_Input() {
                var input = new UnVerifiedDuration(1);
                var expected = input.Verify();

                expected.Should().Be(_pricingEngine.DurationInMinutes(1));
            }

            [Fact]
            public void Duration_Throws_Error_For_Invalid_Input() {
                Action verifyFailing = () => new UnVerifiedDuration(0).Verify();

                Assert.Throws<ArgumentException>(verifyFailing);
            }
        }
    }
}

