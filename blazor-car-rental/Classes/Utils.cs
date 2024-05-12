using blazor_car_rental.Services;
using Blazored.LocalStorage;
using Newtonsoft.Json;
using Timer = System.Timers.Timer;

namespace blazor_car_rental {
    public static class Utils {
        [Flags]
        public enum EditButtonPositions {
            NewColumn = 1 << 0,
            AlwaysVisible = 1 << 1
        }

        public static EditButtonPositions CalculateEditButtonPosition(StateService stateService) {
            var editButtonPosition = stateService.IsTouchDevice
                ? EditButtonPositions.AlwaysVisible
                : 0;
            if (stateService.IsXs) editButtonPosition |= EditButtonPositions.NewColumn;

            return editButtonPosition;
        }

        public static async Task ActuallySaveSettings(StateService stateService, ILocalStorageService localStorage) {
            var serializedSettings = JsonConvert.SerializeObject(stateService.Settings);
            await localStorage.SetItemAsStringAsync("settings", serializedSettings);
        }
        public class MyTimer {
            private readonly Action _toRun;
            public readonly Action Start;
            private Timer _timer = new();

            public MyTimer(Action toRun, int interval) {
                Start = () => {
                    _timer = new();
                    _timer.Elapsed += (_, _) => {
                        try {
                            _toRun?.Invoke();
                        } catch {
                            // ignored
                        }
                    };
                    _timer.Interval = interval;
                    _timer.Enabled = true;
                };

                _toRun = toRun;
            }

            public void Stop() {
                try {
                    _timer.Stop();
                    _timer.Dispose();
                } catch (Exception) {
                    // ignored
                }
            }
        }
        public static class Consts {
            public const string NotRented = "##notRented";
        }
    }
}
