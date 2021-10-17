using CarriesCars.Domain;
using CarriesCars.Domain.PricingEngine;
using FluentAssertions;
using System;
using Xunit;

namespace CarriesCar.Domain.Tests {
    public class GivenADuration {
        
        public class WhenDurationInMinutes : GivenAPricingEngine {

            [Fact]
            public void Guards_Against_Negative_Duration() {
                Action ZeroPricingDurationPassed = () => (-1).Duration();

                Assert.Throws<ArgumentException>(ZeroPricingDurationPassed);
            }

            [Fact]
            public void Duration_Verifies_Valid_Input() {
                var input = new UnVerifiedDuration(1);
                var expected = input.Verify();

                expected.Should().Be(1.Duration());
            }

            [Fact]
            public void Duration_Throws_Error_For_Invalid_Input() {
                Action verifyFailing = () => new UnVerifiedDuration(-5).Verify();

                Assert.Throws<ArgumentException>(verifyFailing);
            }
        }
    }
}

