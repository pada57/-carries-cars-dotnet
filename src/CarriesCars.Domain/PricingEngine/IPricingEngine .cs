using CSharpFunctionalExtensions;

namespace CarriesCars.Domain.PricingEngine
{
    public interface IPricingEngine<T>
    {
        TrustedMoney CalculatePrice(TrustedMoney pricePerMinute, IDuration duration, TrustedMoney reservationExtraPricePerMinute = null, IDuration reservationDuration = null);

        IDuration DurationInMinutes(int minutes);
    }
}
