namespace Md.GoogleCloudFirestore.Tests.Logic
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using Md.GoogleCloud.Base.Contracts.Logic;
    using Md.GoogleCloud.Base.Logic;
    using Md.GoogleCloudFirestore.Logic;
    using Newtonsoft.Json;
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
                    .Select(x => new TestSubObject(Guid.NewGuid().ToString(), x, TestEnum.Second))
                    .ToArray());
            var obj2 = new TestObject(
                obj1.Foo,
                Enumerable.Range(0, 3)
                    .Select(x => new TestSubObject(Guid.NewGuid().ToString(), x, TestEnum.Second))
                    .ToArray());
            await database.InsertAsync(obj1);
            await database.InsertAsync(obj2);

            var actuals = (await database.ReadManyAsync("foo", obj2.Foo)).ToArray();
            Assert.Equal(2, actuals.Length);
            Assert.True(actuals.All(o => o.Foo == obj2.Foo));
            var actual1 = actuals.First(x => x.Subs.Count() == 4);
            var actual2 = actuals.First(x => x.Subs.Count() == 3);

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

        [Fact(Skip = "Integration")]
        public async void InsertAsyncAndUpdateByDocumentIdAsyncAndReadByDocumentIdAsync()
        {
            var database = DatabaseTests.Create();
            var obj = new TestObject(
                Guid.NewGuid().ToString(),
                Enumerable.Range(0, 3)
                    .Select(x => new TestSubObject(Guid.NewGuid().ToString(), x, TestEnum.Second))
                    .ToArray());
            var documentId = await database.InsertAsync(obj);

            var updates = new Dictionary<string, object> {{"foo", Guid.NewGuid().ToString()}};
            await database.UpdateByDocumentIdAsync(documentId, updates);

            var actual = await database.ReadByDocumentIdAsync(documentId);
            Assert.NotNull(actual);
            Assert.Equal(updates["foo"], actual.Foo);
            Assert.Equal(obj.Subs.Count(), actual.Subs.Count());
            Assert.True(
                obj.Subs.All(x => actual.Subs.Any(y => x.Enum == y.Enum && x.Int == y.Int && x.String == y.String)));
        }

        [Fact(Skip = "Integration")]
        public async void InsertAsyncAndUpdateOneAsyncAndReadByDocumentIdAsync()
        {
            var database = DatabaseTests.Create();
            var obj = new TestObject(
                Guid.NewGuid().ToString(),
                Enumerable.Range(0, 3)
                    .Select(x => new TestSubObject(Guid.NewGuid().ToString(), x, TestEnum.Second))
                    .ToArray());
            var documentId = await database.InsertAsync(obj);

            var updates = new Dictionary<string, object> {{"foo", Guid.NewGuid().ToString()}};
            await database.UpdateOneAsync("foo", obj.Foo, updates);

            var actual = await database.ReadByDocumentIdAsync(documentId);
            Assert.NotNull(actual);
            Assert.Equal(updates["foo"], actual.Foo);
            Assert.Equal(obj.Subs.Count(), actual.Subs.Count());
            Assert.True(
                obj.Subs.All(x => actual.Subs.Any(y => x.Enum == y.Enum && x.Int == y.Int && x.String == y.String)));
        }

        private static IDatabase<TestObject> Create()
        {
            var json = File.ReadAllText("appsettings.json");
            var configuration = JsonConvert.DeserializeObject<DatabaseConfiguration>(json);
            Assert.NotNull(configuration);
            return new Database<TestObject>(configuration, TestObject.FromDictionary);
        }
    }
}
