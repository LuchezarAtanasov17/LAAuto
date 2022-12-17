using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace LAAuto.Entities
{
    /// <summary>
    /// Represents a converter between timespan and time only values.
    /// </summary>
    internal class TimeOnlyToTimeSpanConverter : ValueConverter<TimeOnly, TimeSpan>
    {
        /// <summary>
        /// Initialize a new instance of the <see cref="TimeOnlyToTimeSpanConverter"/> class.
        /// </summary>
        public TimeOnlyToTimeSpanConverter() : base(
               x => x.ToTimeSpan(),
               y => TimeOnly.FromTimeSpan(y))
        {

        }
    }
}
