namespace blazor_car_rental.Services {
    public class StateService {
        private bool _isEdited;

        private bool _isXs;
        private bool _saveButtonLoading;
        private bool _saveButtonLoadingText = true;

        public List<Customer> Customers = new();

        public bool IsTouchDevice = false;

        public Settings Settings = new();

        public bool IsEdited {
            get => _isEdited;
            set {
                _isEdited = value;
                _EditedUpdate();
            }
        }
        public bool SaveButtonLoading {
            get => _saveButtonLoading;
            set {
                _saveButtonLoading = value;
                MainLayoutUpdate?.Invoke();
            }
        }
        public bool SaveButtonLoadingText {
            get => _saveButtonLoadingText;
            set {
                _saveButtonLoadingText = value;
                SaveButtonLoading = value;
            }
        }
        public bool IsXs {
            get => _isXs;
            set {
                _isXs = value;
                IsXsUpdate?.Invoke();
            }
        }

        public void Edited() {
            IsEdited = true;
            if (Settings.AutoSave) TryAutoSave?.Invoke();
        }

        /// <remarks>
        ///     Used internally
        /// </remarks>
        private void _EditedUpdate() {
            MainLayoutUpdate?.Invoke();
            EditedUpdate?.Invoke();
        }
        public event Action? MainLayoutUpdate;
        public event Action? EditedUpdate;
        public event Action? TryAutoSave;
        public event Action? IsXsUpdate;
        public event Action? SettingsChanged;
        public void ApplyChangedSettings() {
            SettingsChanged?.Invoke();
        }
        public void FixImportedCustomers() {
            foreach (var customer in Customers) {
                foreach (var customerRentedCar in customer.RentedCars) {
                    customerRentedCar.RentedTo = customer;
                }
            }

            foreach (var notSoRentedCar in Customers.SelectMany(x => x.RentedCars)
                         .Where(x => x.RentState == Car.RentStates.NotRented).ToList()) {
                notSoRentedCar.Return(this);
            }
        }
    }
}
