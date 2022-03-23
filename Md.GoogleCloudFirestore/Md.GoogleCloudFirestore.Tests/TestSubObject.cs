namespace Md.GoogleCloudFirestore.Tests
{
    using System.Collections.Generic;
    using Md.Common.Extensions;
    using Md.GoogleCloud.Base.Logic;

    public class TestSubObject : ToDictionaryConverter
    {
        public TestSubObject(string s, int i, TestEnum e)
        {
            this.String = s.ValidateIsNotNullOrWhitespace(nameof(s));
            this.Int = i;
            this.Enum = e.IsDefined(nameof(e));
        }

        public TestEnum Enum { get; }

        public int Int { get; }

        public string String { get; }

        public override IDictionary<string, object> AddToDictionary(IDictionary<string, object> dictionary)
        {
            dictionary.Add("enum", this.Enum.ToDatabase());
            dictionary.Add("int", this.Int);
            dictionary.Add("string", this.String);
            return dictionary;
        }

        public static TestSubObject FromDictionary(IDictionary<string, object> dictionary)
        {
            var enumValue = dictionary.GetEnumValue<TestEnum>("enum");
            var intValue = dictionary.GetInt("int");
            var stringValue = dictionary.GetString("string");
            return new TestSubObject(stringValue, intValue, enumValue);
        }
    }
}
