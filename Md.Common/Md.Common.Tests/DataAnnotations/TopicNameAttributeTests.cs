namespace Md.Common.Tests.DataAnnotations
{
    using System.ComponentModel.DataAnnotations;
    using Md.Common.DataAnnotations;
    using Xunit;

    public class TopicNameAttributeTests
    {
        [Theory]
        [InlineData("", true)]
        [InlineData("A", true)]
        [InlineData("A_", false)]
        [InlineData("A_B", true)]
        [InlineData("A_TOPIC_NAME", true)]
        [InlineData("a_topic_name", false)]
        [InlineData("A1_B", false)]
        public void Validate(string input, bool success)
        {
            var result = new TopicNameAttribute().GetValidationResult(input, new ValidationContext(input));
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
