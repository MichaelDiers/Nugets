namespace Md.Common.Tests.DataAnnotations
{
    using System.ComponentModel.DataAnnotations;
    using Md.Common.DataAnnotations;
    using Xunit;

    public class ProjectIdAttributeTests
    {
        [Theory]
        [InlineData("", true)]
        [InlineData("a", true)]
        [InlineData("a-", false)]
        [InlineData("a-b", true)]
        [InlineData("a-project-id", true)]
        [InlineData("A-PROJECT", false)]
        [InlineData("1project", false)]
        public void Validate(string input, bool success)
        {
            var result = new ProjectIdAttribute().GetValidationResult(input, new ValidationContext(input));
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
