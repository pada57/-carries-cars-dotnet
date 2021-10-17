using Ardalis.GuardClauses;
using CSharpFunctionalExtensions;
using System;
using System.Collections.Generic;

namespace CarriesCars.Domain
{
    public class TrustedMoney : ValueObject
    {
        public TrustedMoney(double amount, string currencyIsoCode)
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

        public TrustedMoney MultiplyAndRound(double multiplier) {
            var multipliedAmount = Amount * multiplier;
            var rounded = Convert.ToInt32(multipliedAmount);
            return new TrustedMoney(rounded, CurrencyIsoCode);
        }

        
        public TrustedMoney Add(TrustedMoney right) {
            Guard.Against.Null(right, nameof(right));
            if (! CurrencyIsoCode.Equals(right.CurrencyIsoCode, StringComparison.InvariantCultureIgnoreCase)) 
                throw new InvalidOperationException("Cannot add two money with different currencies");

            return new TrustedMoney(Amount + right.Amount, CurrencyIsoCode);
        }

        public static TrustedMoney operator +(TrustedMoney left, TrustedMoney right) {
            Guard.Against.Null(left, nameof(right));

            return left.Add(right);
        }
    }


    public static class MoneyExtensions
    {
        public static TrustedMoney EUR(this int amount) => new TrustedMoney(amount, "EUR");
        public static TrustedMoney EUR(this double amount) => new TrustedMoney(amount, "EUR");

        public static TrustedMoney USD(this int amount) => new TrustedMoney(amount, "USD");
        public static TrustedMoney USD(this double amount) => new TrustedMoney(amount, "USD");
    }
}
