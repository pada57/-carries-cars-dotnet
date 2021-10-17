using Ardalis.GuardClauses;
using CSharpFunctionalExtensions;
using System;
using System.Collections.Generic;

namespace CarriesCars.Domain
{
    public class Money : ValueObject
    {
        public Money(double amount, string currencyIsoCode)
        {
            Amount = amount;
            CurrencyIsoCode = currencyIsoCode;
        }

        public double Amount { get; }

        public string CurrencyIsoCode { get; }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Amount;
            yield return CurrencyIsoCode;
        }

        public Money MultiplyAndRound(double multiplier) {
            var multipliedAmount = Amount * multiplier;
            var rounded = Convert.ToInt32(multipliedAmount);
            return new Money(rounded, CurrencyIsoCode);
        }

        
        public Money Add(Money right) {
            Guard.Against.Null(right, nameof(right));
            if (! CurrencyIsoCode.Equals(right.CurrencyIsoCode, StringComparison.InvariantCultureIgnoreCase)) 
                throw new InvalidOperationException("Cannot add two money with different currencies");

            return new Money(Amount + right.Amount, CurrencyIsoCode);
        }

        public static Money operator +(Money left, Money right) {
            Guard.Against.Null(left, nameof(right));

            return left.Add(right);
        }
    }


    public static class MoneyExtensions
    {
        public static Money EUR(this int amount) => new Money(amount, "EUR");
        public static Money EUR(this double amount) => new Money(amount, "EUR");

        public static Money USD(this int amount) => new Money(amount, "USD");
        public static Money USD(this double amount) => new Money(amount, "USD");
    }
}
