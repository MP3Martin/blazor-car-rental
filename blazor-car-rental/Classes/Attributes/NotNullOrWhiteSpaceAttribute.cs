using System.ComponentModel.DataAnnotations;

namespace blazor_car_rental.Attributes {
    public class NotNullOrWhiteSpaceAttribute : ValidationAttribute {
        public NotNullOrWhiteSpaceAttribute() : base("This field is required") { }

        public override bool IsValid(object? value) {
            return value switch {
                null => false,
                string str => !string.IsNullOrWhiteSpace(str),
                _ => true
            };
        }
    }
}
