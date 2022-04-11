namespace Md.Common.Database
{
    using System;
    using System.Collections.Generic;
    using Md.Common.Contracts.Database;
    using Md.Common.Extensions;
    using Md.Common.Model;
    using Newtonsoft.Json;

    /// <summary>
    ///     Basis database object.
    /// </summary>
    public class DatabaseObject : ToDictionaryConverter, IDatabaseObject
    {
        /// <summary>
        ///     The name of the created field.
        /// </summary>
        public const string CreatedName = "created";

        /// <summary>
        ///     The json name of the document id.
        /// </summary>
        public const string DocumentIdName = "documentId";

        /// <summary>
        ///     The json name of <see cref="ParentDocumentId" />.
        /// </summary>
        public const string ParentDocumentIdName = "parentDocumentId";

        /// <summary>
        ///     Creates a new instance of <see cref="DatabaseObject" />.
        /// </summary>
        public DatabaseObject()
            : this(string.Empty, null, string.Empty)
        {
        }

        /// <summary>
        ///     Creates a new instance of <see cref="DatabaseObject" />.
        /// </summary>
        /// <param name="documentId">The id of the document.</param>
        /// <param name="created">The created time of the object.</param>
        /// <param name="parentDocumentId">The document id of the logical parent.</param>
        [JsonConstructor]
        public DatabaseObject(string? documentId, DateTime? created, string? parentDocumentId)
        {
            this.DocumentId = string.IsNullOrWhiteSpace(documentId)
                ? string.Empty
                : documentId.ValidateIsAGuid(nameof(documentId));
            this.Created = created ?? DateTime.MinValue;
            this.ParentDocumentId = string.IsNullOrWhiteSpace(parentDocumentId)
                ? string.Empty
                : parentDocumentId.ValidateIsAGuid(nameof(parentDocumentId));
        }

        /// <summary>
        ///     Gets the created field of the database object.
        /// </summary>
        [JsonProperty(DatabaseObject.CreatedName, Required = Required.AllowNull, Order = 2)]
        public DateTime Created { get; }

        /// <summary>
        ///     Gets the id of the document.
        /// </summary>
        [JsonProperty(DatabaseObject.DocumentIdName, Required = Required.AllowNull, Order = 1)]
        public string DocumentId { get; }

        /// <summary>
        ///     Gets the document id of the logical parent document.
        /// </summary>
        [JsonProperty(DatabaseObject.ParentDocumentIdName, Required = Required.AllowNull, Order = 3)]
        public string ParentDocumentId { get; }

        /// <summary>
        ///     Add the property values to a dictionary.
        /// </summary>
        /// <param name="dictionary">The values are added to the given dictionary.</param>
        /// <returns>The given <paramref name="dictionary" />.</returns>
        public override IDictionary<string, object> AddToDictionary(IDictionary<string, object> dictionary)
        {
            if (this.DocumentId != null)
            {
                dictionary.Add(DatabaseObject.DocumentIdName, this.DocumentId);
            }

            if (this.Created != null)
            {
                dictionary.Add(DatabaseObject.CreatedName, this.Created);
            }

            if (this.ParentDocumentId != null)
            {
                dictionary.Add(DatabaseObject.ParentDocumentIdName, this.ParentDocumentId);
            }

            return dictionary;
        }

        /// <summary>
        ///     Create new instance of <see cref="DatabaseObject" />.
        /// </summary>
        /// <param name="dictionary">The data from that the object is created.</param>
        /// <returns>An instance of <see cref="IDatabaseObject" />.</returns>
        public static IDatabaseObject FromDictionary(IDictionary<string, object> dictionary)
        {
            var documentId = dictionary.GetString(DatabaseObject.DocumentIdName);
            var created = dictionary.GetDateTime(DatabaseObject.CreatedName);
            var parentDocumentId = dictionary.GetString(DatabaseObject.ParentDocumentIdName, string.Empty);
            return new DatabaseObject(documentId, created, parentDocumentId);
        }
    }
}
