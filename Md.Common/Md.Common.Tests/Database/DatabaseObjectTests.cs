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
        public void ImplementsIDatabaseObject()
        {
            Assert.IsAssignableFrom<IDatabaseObject>(new DatabaseObject());
            Assert.IsAssignableFrom<IDatabaseObject>(new DatabaseObject(null, null, null));
        }

        [Theory]
        [InlineData(null, null, null)]
        [InlineData("416c5549-4e41-45eb-8e28-4a2300f062ac", null, "91a85553-9698-4c42-93a8-f571ced80da6")]
        public void SerializeDeserialize(string? documentId, DateTime? created, string? parentDocumentId)
        {
            var obj = new DatabaseObject(documentId, created, parentDocumentId);
            Assert.Equal(documentId, obj.DocumentId);
            Assert.Equal(created, obj.Created);
            Assert.Equal(parentDocumentId, obj.ParentDocumentId);

            var fromJson =
                Serializer.DeserializeObject<DatabaseObject>(Serializer.SerializeObject(obj)) as IDatabaseObject;
            Assert.Equal(documentId, fromJson.DocumentId);
            Assert.Equal(created, fromJson.Created);
            Assert.Equal(parentDocumentId, fromJson.ParentDocumentId);
        }
    }
}
