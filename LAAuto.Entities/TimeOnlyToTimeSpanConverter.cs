using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace LAAuto.Entities
{
    internal class TimeOnlyToTimeSpanConverter : ValueConverter<TimeOnly, TimeSpan>
    {
        public TimeOnlyToTimeSpanConverter() : base(
               x => x.ToTimeSpan(),
               y => TimeOnly.FromTimeSpan(y))
        {

        }
    }
}
