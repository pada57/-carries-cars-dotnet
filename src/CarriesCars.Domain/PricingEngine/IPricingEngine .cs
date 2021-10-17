using CSharpFunctionalExtensions;

namespace CarriesCars.Domain.PricingEngine
{
    public interface IPricingEngine<T>
    {
        Money CalculatePrice(Money pricePerMinute, IDuration duration, Money reservationExtraPricePerMinute = null, IDuration reservationDuration = null);

        IDuration DurationInMinutes(int minutes);
    }
}
