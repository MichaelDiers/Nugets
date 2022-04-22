namespace Md.Common.DataAnnotations
{
    using System;
    using System.ComponentModel.DataAnnotations;

    /// <summary>
    ///     The validator checks if the enum value is defined and has a non-zero value.
    /// </summary>
    public class NonZeroAndDefinedEnumAttribute : ValidationAttribute
    {
        /// <summary>
        ///     The type of the enum that is validated.
        /// </summary>
        private readonly Type enumType;

        /// <summary>
        ///     Initializes a new instance of the <see cref="NonZeroAndDefinedEnumAttribute" /> class.
        /// </summary>
        /// <param name="enumType">The type of the enum that is validated.</param>
        public NonZeroAndDefinedEnumAttribute(Type enumType)
            : base("{0} enum value is not defined or 0.")
        {
            this.enumType = enumType;
        }

        /// <summary>Validates the specified value with respect to the current validation attribute.</summary>
        /// <param name="value">The value to validate.</param>
        /// <param name="validationContext">The context information about the validation operation.</param>
        /// <returns>An instance of the <see cref="T:System.ComponentModel.DataAnnotations.ValidationResult" /> class.</returns>
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            try
            {
                if (!Enum.IsDefined(this.enumType, value) || (int) value == 0)
                {
                    return new ValidationResult(this.FormatErrorMessage(validationContext.DisplayName));
                }

                return ValidationResult.Success;
            }
            catch
            {
                return new ValidationResult(this.FormatErrorMessage(validationContext.DisplayName));
            }
        }
    }
}
