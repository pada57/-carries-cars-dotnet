using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarriesCars.Domain.PricingEngine {
    public static class Constants {
        //TODO move this reading from file or env

        public const double DefaultRidePricePerMinute = 0.24;        
        public const double DefaultReservationPricePerMinute = 0.24;

        public static readonly IDuration FreeReservationDuration = 20.Duration();
    }
}
