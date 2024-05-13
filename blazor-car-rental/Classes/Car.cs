using blazor_car_rental.Attributes;
using blazor_car_rental.Services;
using Newtonsoft.Json;
using static blazor_car_rental.Utils.Consts;

namespace blazor_car_rental {
    [JsonObject(MemberSerialization.OptIn)]
    public record Car : ITableItem {
        public enum RentStates {
            NotRented,
            Rented
        }

        [JsonProperty]
        public int CostPerDay { get; set; }

        [JsonProperty]
        [NotNullOrWhiteSpace]
        public string LicensePlate { get; set; } = string.Empty;

        [JsonProperty]
        [JsonRequired]
        [NotNullOrWhiteSpace]
        public string Manufacturer { get; set; } = string.Empty;

        [JsonProperty]
        public int Mileage { get; set; }

        [JsonProperty]
        [JsonRequired]
        [NotNullOrWhiteSpace]
        public string Model { get; set; } = string.Empty;

        [JsonProperty]
        public short ProductionYear { get; set; } = (short)DateTime.Today.Year;

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public RentalRecord? RentalRecord { get; set; }

        public Customer? RentedTo { get; set; }

        [JsonProperty]
        public int SeatCount { get; set; }

        [JsonProperty]
        [NotNullOrWhiteSpace]
        // ReSharper disable once InconsistentNaming
        public string VIN { get; set; } = string.Empty;

        public RentStates RentState => (RentedTo?.Name ?? NotRented) == NotRented ? RentStates.NotRented : RentStates.Rented;
        public string Name => $"{Manufacturer} {Model}";
        public string DisplayName => Name;

        /// <summary>
        ///     Rents this car to a customer
        /// </summary>
        public void RentTo(StateService stateService, Customer customer, RentalRecord rentalRecord, Car? oldCar = null) {
            foreach (var customerItem in stateService.Customers) {
                customerItem.RentedCars.Remove(this);
                if (oldCar is not null) customerItem.RentedCars.Remove(oldCar);
            }

            RentedTo = customer;
            RentalRecord = rentalRecord;

            customer.RentedCars.Add(this);
        }

        /// <summary>
        ///     Returns this car
        /// </summary>
        public void Return(StateService stateService, Car? oldCar = null) {
            foreach (var customer in stateService.Customers) {
                customer.RentedCars.Remove(this);
                if (oldCar is not null) customer.RentedCars.Remove(oldCar);
            }

            RentedTo = stateService.Customers.First(x => x.Name == NotRented);
            RentalRecord = null;
            RentedTo.RentedCars.Remove(this);
            if (oldCar is not null) RentedTo.RentedCars.Remove(this);
            stateService.Customers.First(x => x.Name == NotRented).RentedCars.Add(this);
        }
    }
}
