namespace Md.Common.Tests.DataAnnotations
{
    using System.ComponentModel.DataAnnotations;
    using Md.Common.DataAnnotations;
    using Xunit;

    public class NonZeroAndDefinedEnumAttributeTests
    {
        [Theory]
        [InlineData(TestEnum.None, false)]
        [InlineData(TestEnum.Foo, true)]
        [InlineData(TestEnum.Bar, true)]
        [InlineData((TestEnum) 100, false)]
        public void Validate(TestEnum testEnum, bool success)
        {
            var result = new NonZeroAndDefinedEnumAttribute(typeof(TestEnum)).GetValidationResult(
                testEnum,
                new ValidationContext(testEnum));
            if (success)
            {
                Assert.Equal(ValidationResult.Success, result);
            }
            else
            {
                Assert.NotEqual(ValidationResult.Success, result);
            }
        }
    }
}
