namespace Md.Common.Contracts.Database
{
    using System;
    using Md.Common.Contracts.Model;

    /// <summary>
    ///     Describes a database object.
    /// </summary>
    public interface IDatabaseObject : IToDictionary
    {
        /// <summary>
        ///     Gets the created field of the database object.
        /// </summary>
        DateTime Created { get; }

        /// <summary>
        ///     Gets the id of the document.
        /// </summary>
        string DocumentId { get; }

        /// <summary>
        ///     Gets the document id of the logical parent document.
        /// </summary>
        string ParentDocumentId { get; }
    }
}
