using blazor_car_rental.Attributes;
using blazor_car_rental.Services;
using Newtonsoft.Json;

namespace blazor_car_rental {
    [JsonObject(MemberSerialization.OptIn)]
    public record Customer(StateService StateService, string Name = "") : ITableItem {
        private StateService _stateService = StateService;
        [JsonProperty] public List<Car> RentedCars = new();

        [JsonProperty]
        [JsonRequired]
        [NotNullOrWhiteSpace]
        [CantStartWithSpecialChars(ErrorMessage = @"Name can't start with ""##"" (because i said so 😎)")]
        [NoDuplicateCustomer]
        public string Name { get; set; } = Name;

        public string DisplayName => Name switch {
            Utils.Consts.NotRented => "Not rented",
            _ => Name
        };

        public override string ToString() {
            return DisplayName;
        }
    }
}
