namespace Md.GoogleCloudFirestore.Tests
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Md.Common.Contracts.Database;
    using Md.Common.Extensions;
    using Md.Common.Model;

    public class TestObject : ToDictionaryConverter, IDatabaseObject
    {
        public TestObject(string foo, IEnumerable<TestSubObject> subs)
        {
            this.Foo = foo.ValidateIsNotNullOrWhitespace(nameof(foo));
            this.Subs = subs;
        }

        public string Foo { get; }

        public IEnumerable<TestSubObject> Subs { get; }

        public DateTime Created { get; } = DateTime.Now;
        public string DocumentId { get; } = Guid.NewGuid().ToString();
        public string ParentDocumentId { get; } = Guid.NewGuid().ToString();

        public override IDictionary<string, object> AddToDictionary(IDictionary<string, object> dictionary)
        {
            dictionary.Add("foo", this.Foo);
            dictionary.Add("subs", this.Subs.Select(x => x.ToDictionary()));
            return dictionary;
        }

        public static TestObject FromDictionary(IDictionary<string, object> dictionary)
        {
            var foo = dictionary.GetString("foo");
            var subs = dictionary.GetDictionaries("subs").Select(TestSubObject.FromDictionary).ToArray();
            return new TestObject(foo, subs);
        }
    }
}
