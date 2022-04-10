namespace Md.Common.Tests.Model
{
    using Md.Common.Contracts;
    using Md.Common.Model;
    using Xunit;

    public class RuntimeEnvironmentTests
    {
        [Theory]
        [InlineData(Environment.Test, "projectId")]
        public void Ctor(Environment environment, string projectId)
        {
            var actual = new RuntimeEnvironment {Environment = environment, ProjectId = projectId};
            Assert.Equal(environment, actual.Environment);
            Assert.Equal(projectId, actual.ProjectId);

            var iActual = actual as IRuntimeEnvironment;
            Assert.Equal(environment, iActual.Environment);
            Assert.Equal(projectId, iActual.ProjectId);
        }

        [Fact]
        public void ImplementsIRuntimeEnvironment()
        {
            Assert.IsAssignableFrom<IRuntimeEnvironment>(new RuntimeEnvironment());
        }
    }
}
