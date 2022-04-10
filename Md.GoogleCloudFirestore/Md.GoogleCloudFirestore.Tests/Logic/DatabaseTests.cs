namespace Md.GoogleCloudFirestore.Tests.Logic
{
    using System;
    using System.IO;
    using System.Linq;
    using Md.Common.Logic;
    using Md.GoogleCloudFirestore.Contracts.Logic;
    using Md.GoogleCloudFirestore.Logic;
    using Md.GoogleCloudFirestore.Model;
    using Xunit;

    public class DatabaseTests
    {
        [Fact(Skip = "Integration")]
        public async void InsertAsyncAndReadByDocumentIdAsync()
        {
            var database = DatabaseTests.Create();
            var obj = new TestObject(
                Guid.NewGuid().ToString(),
                Enumerable.Range(0, 3)
                    .Select(x => new TestSubObject(Guid.NewGuid().ToString(), x, TestEnum.Second))
                    .ToArray());
            var documentId = await database.InsertAsync(obj);
            var actual = await database.ReadByDocumentIdAsync(documentId);
            Assert.NotNull(actual);
            Assert.Equal(obj.Foo, actual.Foo);
            Assert.Equal(obj.Subs.Count(), actual.Subs.Count());
            Assert.True(
                obj.Subs.All(x => actual.Subs.Any(y => x.Enum == y.Enum && x.Int == y.Int && x.String == y.String)));
        }

        [Fact(Skip = "Integration")]
        public async void InsertAsyncAndReadManyAsync()
        {
            var database = DatabaseTests.Create();
            var obj1 = new TestObject(
                Guid.NewGuid().ToString(),
                Enumerable.Range(0, 4)
                    .Select(x => new TestSubObject(Guid.NewGuid().ToString(), x, TestEnum.First))
                    .ToArray());
            var obj2 = new TestObject(
                obj1.Foo,
                Enumerable.Range(0, 3)
                    .Select(x => new TestSubObject(Guid.NewGuid().ToString(), x, TestEnum.Second))
                    .ToArray());
            await database.InsertAsync(obj1);
            await database.InsertAsync(obj2);

            var actualArray = (await database.ReadManyAsync("foo", obj2.Foo)).ToArray();
            Assert.Equal(2, actualArray.Length);
            Assert.True(actualArray.All(o => o.Foo == obj2.Foo));
            var actual1 = actualArray.First(x => x.Subs.Count() == 4);
            var actual2 = actualArray.First(x => x.Subs.Count() == 3);

            Assert.Equal(obj1.Subs.Count(), actual1.Subs.Count());
            Assert.Equal(obj2.Subs.Count(), actual2.Subs.Count());

            Assert.True(
                obj1.Subs.All(x => actual1.Subs.Any(y => x.Enum == y.Enum && x.Int == y.Int && x.String == y.String)));
            Assert.True(
                obj2.Subs.All(x => actual2.Subs.Any(y => x.Enum == y.Enum && x.Int == y.Int && x.String == y.String)));
        }

        [Fact(Skip = "Integration")]
        public async void InsertAsyncAndReadOneAsync()
        {
            var database = DatabaseTests.Create();
            var obj = new TestObject(
                Guid.NewGuid().ToString(),
                Enumerable.Range(0, 3)
                    .Select(x => new TestSubObject(Guid.NewGuid().ToString(), x, TestEnum.Second))
                    .ToArray());
            var _ = await database.InsertAsync(obj);
            var actual = await database.ReadOneAsync("foo", obj.Foo);
            Assert.NotNull(actual);
            Assert.Equal(obj.Foo, actual.Foo);
            Assert.Equal(obj.Subs.Count(), actual.Subs.Count());
            Assert.True(
                obj.Subs.All(x => actual.Subs.Any(y => x.Enum == y.Enum && x.Int == y.Int && x.String == y.String)));
        }

        private static IDatabase<TestObject> Create()
        {
            // ReSharper disable once StringLiteralTypo
            var json = File.ReadAllText("appsettings.json");
            var configuration = Serializer.DeserializeObject<DatabaseConfiguration>(json);
            Assert.NotNull(configuration);
            return new Database<TestObject>(configuration, TestObject.FromDictionary);
        }
    }
}
