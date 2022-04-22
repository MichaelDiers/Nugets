namespace Md.Common.DataAnnotations
{
    using System;
    using System.ComponentModel.DataAnnotations;

    /// <summary>
    ///     Regular expression validator for pub/sub topic names, like MY_TOPIC_NAME.
    /// </summary>
    [AttributeUsage(AttributeTargets.Property)]
    public class TopicNameAttribute : RegularExpressionAttribute
    {
        /// <summary>Initializes a new instance of the <see cref="TopicNameAttribute" /> class.</summary>
        public TopicNameAttribute()
            : base("^[A-Z]+(_[A-Z]+)*$")
        {
        }
    }
}
