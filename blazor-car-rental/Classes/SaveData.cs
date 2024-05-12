using Newtonsoft.Json;

namespace blazor_car_rental {
    public struct SaveData {
        [JsonRequired] public Settings Settings;
        [JsonRequired] public List<Customer> Customers;
    }
}
