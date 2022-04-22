namespace Md.Common.Model
{
    using System.ComponentModel.DataAnnotations;
    using Md.Common.Contracts.Model;
    using Md.Common.DataAnnotations;

    /// <summary>
    ///     Describes the runtime environment.
    /// </summary>
    public class RuntimeEnvironment : IRuntimeEnvironment
    {
        /// <summary>
        ///     Gets the environment.
        /// </summary>
        [Required]
        [NonZeroAndDefinedEnum(typeof(Environment))]
        public Environment Environment { get; set; } = Environment.None;

        /// <summary>
        ///     Gets the id of the project.
        /// </summary>
        [Required]
        [RegularExpression("^[a-z0-9]+(-[a-z0-9]+)*$")]
        public string ProjectId { get; set; } = string.Empty;
    }
}
