namespace Md.GoogleCloudFirestore.Tests
{
    using System.Collections.Generic;
    using System.Linq;
    using Md.Common.Extensions;
    using Md.GoogleCloud.Base.Logic;

    public class TestObject : ToDictionaryConverter
    {
        public TestObject(string foo, IEnumerable<TestSubObject> subs)
        {
            this.Foo = foo.ValidateIsNotNullOrWhitespace(nameof(foo));
            this.Subs = subs;
        }

        public string Foo { get; }

        public IEnumerable<TestSubObject> Subs { get; }

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
