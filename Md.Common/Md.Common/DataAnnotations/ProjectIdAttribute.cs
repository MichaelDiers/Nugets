namespace Md.Common.DataAnnotations
{
    using System.ComponentModel.DataAnnotations;

    /// <summary>
    ///     Validator for project ids, like project-id.
    /// </summary>
    public class ProjectIdAttribute : RegularExpressionAttribute
    {
        /// <summary>Initializes a new instance of the <see cref="ProjectIdAttribute" /> class.</summary>
        public ProjectIdAttribute()
            : base("^[a-z][a-z0-9]*(-[a-z][a-z0-9]*)*$")
        {
        }
    }
}
