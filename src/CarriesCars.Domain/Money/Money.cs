using Ardalis.GuardClauses;
using CSharpFunctionalExtensions;
using System;
using System.Collections.Generic;

namespace CarriesCars.Domain
{
    public class Money : ValueObject
    {
        public Money(double amount, Currency currency)
        {
            Amount = amount;
            Currency = currency;
        }

        public double Amount { get; }

        public Currency Currency { get; }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Amount;
            yield return Currency.IsoCode;
        }

        public Money MultiplyAndRound(double multiplier) {
            var multipliedAmount = Amount * multiplier;
            var rounded = Convert.ToInt32(multipliedAmount);
            return new Money(rounded, Currency);
        }

        
        public Money Add(Money right) {
            Guard.Against.Null(right, nameof(right));
            if (! Currency.IsoCode.Equals(right.Currency.IsoCode, StringComparison.InvariantCultureIgnoreCase)) 
                throw new InvalidOperationException("Cannot add two money with different currencies");

            return new Money(Amount + right.Amount, Currency);
        }

        public static Money operator +(Money left, Money right) {
            Guard.Against.Null(left, nameof(right));

            return left.Add(right);
        }
    }


    public static class MoneyExtensions
    {
        public static Currency Ccy(this string isoCode) => new Currency(isoCode);

        public static Money EUR(this int amount) => new Money(amount, "EUR".Ccy());
        public static Money EUR(this double amount) => new Money(amount, "EUR".Ccy());

        public static Money USD(this int amount) => new Money(amount, "USD".Ccy());
        public static Money USD(this double amount) => new Money(amount, "USD".Ccy());
    }
}
