namespace CarriesCars.Domain.PricingEngine {
    public static class Constants {
        //TODO move this reading from file or env

        public const double DefaultRidePricePerMinute = 0.24;
        public const double DefaultReservationPricePerMinute = 0.09;
        public const double DefaultChargePricePerKilometer = 0.19;
        public const int DefaultMaxRideWithoutCharges = 250;

        public static readonly IDuration FreeReservationDuration = 20.Duration();
    }
}
