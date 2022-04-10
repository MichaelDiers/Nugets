namespace Md.Common.Tests.Logic
{
    using System;
    using Md.Common.Logic;
    using Xunit;
    using Environment = Md.Common.Contracts.Model.Environment;

    public class SerializerTests
    {
        [Theory]
        [InlineData(Environment.Test, 10)]
        [InlineData(Environment.Stage, 11)]
        [InlineData(Environment.Prod, 12)]
        [InlineData(Environment.None, 13)]
        public void DeserializeObject(Environment environment, int value)
        {
            var json = Serializer.SerializeObject(
                new SerializerTestsObject {Environment = environment, TestInt = value});
            var actual = Serializer.DeserializeObject<SerializerTestsObject>(json);
            Assert.Equal(environment, actual.Environment);
            Assert.Equal(value, actual.TestInt);
        }

        [Fact]
        public void DeserializeObjectThrowsArgumentException()
        {
            Assert.Throws<ArgumentException>(() => Serializer.DeserializeObject<SerializerTestsObject>(string.Empty));
        }

        [Theory]
        [InlineData(Environment.Test, 10)]
        public void SerializeObject(Environment environment, int value)
        {
            var expected = $"{{\"Environment\":\"{environment.ToString()}\",\"TestInt\":{value}}}";
            var actual =
                Serializer.SerializeObject(new SerializerTestsObject {Environment = environment, TestInt = value});
            Assert.Equal(expected, actual);
        }
    }
}
