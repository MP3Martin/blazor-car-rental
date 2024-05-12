using System.ComponentModel.DataAnnotations;

namespace blazor_car_rental.Attributes {
    /// <remarks>Can only be used on a string that is a property of <see cref="Customer" /> </remarks>
    [AttributeUsage(AttributeTargets.Property)]
    public class NoDuplicateCustomerAttribute : ValidationAttribute {
        public NoDuplicateCustomerAttribute() : base("This customer name is already registered") { }

        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext) {
            try {
                if (value is not string || validationContext.ObjectInstance is not Customer customer) {
                    return new("", new[] { validationContext.MemberName }!);
                }

                if (customer.StateService.Customers.All(x => x.Name != (string)value)) {
                    return null;
                }

                return new(ErrorMessage, new[] { validationContext.MemberName }!);
            } catch {
                return null;
            }
        }
    }
}
