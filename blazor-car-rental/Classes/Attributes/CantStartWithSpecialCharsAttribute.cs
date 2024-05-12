using System.ComponentModel.DataAnnotations;

namespace blazor_car_rental.Attributes {
    public class CantStartWithSpecialCharsAttribute : ValidationAttribute {
        public override bool IsValid(object? value) {
            return value switch {
                null => false,
                string str => !str.StartsWith("##"),
                _ => true
            };
        }
    }
}
