namespace CarriesCars.Domain.PricingEngine {
    public interface IPricingEngine {
        Money CalculatePrice(CarRide carRide);
    }
}
