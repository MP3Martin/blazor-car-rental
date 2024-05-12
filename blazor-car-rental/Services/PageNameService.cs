// Thanks to https://stackoverflow.com/users/41403/quango @ https://stackoverflow.com/a/60951797/10518428
// for explaining how to use services.

namespace blazor_car_rental.Services {
    public class PageNameService {
        public string CurrentPageName { get; private set; } = "Loading...";

        public void SetCurrentPageName(string name) {
            if (CurrentPageName == name) return;
            CurrentPageName = name;
            NotifyStateChanged();
        }

        public event Action? OnChange;

        private void NotifyStateChanged() {
            OnChange?.Invoke();
        }
    }
}
