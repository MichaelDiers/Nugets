namespace Md.Common.Tests.Logic
{
    using System;
    using Md.Common.Contracts;
    using Md.Common.Logic;
    using Xunit;
    using Environment = Md.Common.Contracts.Environment;

    public class SerializerTests
    {
        [Theory]
        [InlineData(Environment.Test, 10)]
        [InlineData(Environment.Stage, 11)]
        [InlineData(Environment.Prod, 12)]
        [InlineData(Environment.None, 13)]
        public void DeserializeObject(Environment environment, int value)
        {
            var serializer = new Serializer() as ISerializer;
            var json = serializer.SerializeObject(
                new SerializerTestsObject {Environment = environment, TestInt = value});
            var actual = serializer.DeserializeObject<SerializerTestsObject>(json);
            Assert.Equal(environment, actual.Environment);
            Assert.Equal(value, actual.TestInt);
        }

        [Fact]
        public void DeserializeObjectThrowsArgumentException()
        {
            var serializer = new Serializer() as ISerializer;
            Assert.Throws<ArgumentException>(() => serializer.DeserializeObject<SerializerTestsObject>(string.Empty));
        }

        [Theory]
        [InlineData(Environment.Test, 10)]
        public void SerializeObject(Environment environment, int value)
        {
            var expected = $"{{\"Environment\":\"{environment.ToString()}\",\"TestInt\":{value}}}";
            var serializer = new Serializer() as ISerializer;
            var actual =
                serializer.SerializeObject(new SerializerTestsObject {Environment = environment, TestInt = value});
            Assert.Equal(expected, actual);
        }
    }
}
