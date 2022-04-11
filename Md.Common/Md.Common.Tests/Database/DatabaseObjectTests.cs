namespace Md.Common.Tests.Database
{
    using System;
    using Md.Common.Contracts.Database;
    using Md.Common.Database;
    using Md.Common.Logic;
    using Xunit;

    public class DatabaseObjectTests
    {
        [Fact]
        public void CtorUsingNull()
        {
            var databaseObject = new DatabaseObject();
            Assert.Equal(string.Empty, databaseObject.DocumentId);
            Assert.Equal(string.Empty, databaseObject.ParentDocumentId);
            Assert.Equal(DateTime.MinValue, databaseObject.Created);
        }

        [Fact]
        public void ImplementsIDatabaseObject()
        {
            Assert.IsAssignableFrom<IDatabaseObject>(new DatabaseObject());
            Assert.IsAssignableFrom<IDatabaseObject>(new DatabaseObject(null, null, null));
        }

        [Theory]
        [InlineData(
            "416c5549-4e41-45eb-8e28-4a2300f062ac",
            "2022-04-11T10:40:27.0788464+02:00",
            "91a85553-9698-4c42-93a8-f571ced80da6")]
        public void SerializeDeserialize(string documentId, string created, string parentDocumentId)
        {
            var obj = new DatabaseObject(documentId, DateTime.Parse(created), parentDocumentId);
            Assert.Equal(documentId, obj.DocumentId);
            Assert.Equal(DateTime.Parse(created), obj.Created);
            Assert.Equal(parentDocumentId, obj.ParentDocumentId);

            var fromJson =
                Serializer.DeserializeObject<DatabaseObject>(Serializer.SerializeObject(obj)) as IDatabaseObject;
            Assert.Equal(documentId, fromJson.DocumentId);
            Assert.Equal(DateTime.Parse(created), fromJson.Created);
            Assert.Equal(parentDocumentId, fromJson.ParentDocumentId);
        }
    }
}
