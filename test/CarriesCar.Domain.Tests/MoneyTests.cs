using CarriesCars.Domain;
using System;
using Xunit;

namespace CarriesCar.Domain.Tests
{
    public class GivenAMoney

    {
        public class WhenComparing : GivenAMoney {

            [Fact]
            public void Detect_Equal_Values() {
                Assert.True(99.EUR() == 99.EUR());
                Assert.True(99.EUR().Equals(99.EUR()));
            }

            [Fact]
            public void Detect_Currency_Differences() {
                Assert.True(99.EUR() != 99.USD());
                Assert.False(99.EUR().Equals(99.USD()));
            }

            [Fact]
            public void Detect_Amount_Differences() {
                Assert.True(1.EUR() != 2.USD());
                Assert.False(1.EUR().Equals(2.USD()));
            }
        }

        public class WhenMultiplyAndRound : GivenAMoney {

            [Fact]
            public void Multiplies_Correctly() {
                Assert.True(400.EUR() == 200.EUR().MultiplyAndRound(2.0));
            }

            [Fact]
            public void Multiplies_Correctly_With_Decimals() {
                Assert.True(400.EUR() == 200.05.EUR().MultiplyAndRound(2.0));
            }

            [Fact]
            public void Rounds_Up_Correctly() {
                Assert.True(200.EUR() == 100.EUR().MultiplyAndRound(1.999));
            }

            [Fact]
            public void Rounds_Down_Correctly() {
                Assert.True(199.EUR() == 100.EUR().MultiplyAndRound(1.994));
            }
        }

        public class WhenAdding : GivenAMoney {
            [Fact]
            public void Adds_Correctly() {
                Assert.True(400.EUR() == 200.EUR().Add(200.EUR()));
            }

            [Fact]
            public void Adds_Correctly_using_operator() {
                Assert.True(400.EUR() == 200.EUR() + 200.EUR());
            }

            [Fact]
            public void With_Null_left_operand_throw_exception() {
                Action operation = () => { var result = (Money)null + 200.EUR(); };

                Assert.Throws<ArgumentNullException>(operation);
            }

            [Fact]
            public void With_Null_right_operand_throw_exception() {
                Action operation = () => { var result = 200.EUR() + (Money)null; };

                Assert.Throws<ArgumentNullException>(operation);
            }

            [Fact]
            public void With_method_And_Null_right_operand_throw_exception() {
                Action operation = () => { var result = 200.EUR().Add((Money)null); };

                Assert.Throws<ArgumentNullException>(operation);
            }
        }
    }
}
