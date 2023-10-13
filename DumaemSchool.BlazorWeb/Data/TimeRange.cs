namespace DumaemSchool.BlazorWeb.Data
{
    public class TimeRange
    {
        public required DayOfWeek DayOfWeek { get; init; }
        public required TimeOnly TimeStart { get; init; }
        public required TimeOnly TimeEnd { get; init; }

        public bool Overlaps(TimeRange other)
        {
            return DayOfWeek == other.DayOfWeek && TimeStart < other.TimeEnd && other.TimeStart < TimeEnd;
        }
    }
}
