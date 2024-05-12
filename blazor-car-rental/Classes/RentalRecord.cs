using Newtonsoft.Json;

namespace blazor_car_rental {
    [JsonObject(MemberSerialization.OptIn)]
    public class RentalRecord {
        public RentalRecord(DateTime dateRented) {
            DateRented = dateRented;
        }

        [JsonProperty]
        [JsonRequired]
        public DateTime? DateRented { get; set; }

        public (int hasToPay, int days) CalculatePrice(DateTime returnTime, Car car) {
            if (DateRented is null) return (0, 0);
            var dayCount = (returnTime.Date - ((DateTime)DateRented).Date).Days;
            dayCount = Math.Max(1, dayCount);
            return (car.CostPerDay * dayCount, dayCount);
        }
    }
}
