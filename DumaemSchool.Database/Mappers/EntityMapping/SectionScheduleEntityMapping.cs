using DumaemSchool.Core.OutputModels;
using DumaemSchool.Database.Mappers.EntityMapping.Base;

namespace DumaemSchool.Database.Mappers.EntityMapping;

public sealed class SectionScheduleEntityMapping : AbstractEntityMapping<SectionSchedule>
{
    public SectionScheduleEntityMapping()
    {
        Map(nameof(SectionSchedule.Id), "schedule.id", isPrimaryKey: true);

        Map(nameof(SectionSchedule.SectionId), "sec.id");
        Map(nameof(SectionSchedule.SectionGroupName), "sec.group_name");
        
        Map(nameof(SectionSchedule.DayOfWeek), "schedule.day_of_week");
        Map(nameof(SectionSchedule.Time), "schedule.time");
        Map(nameof(SectionSchedule.Duration), "schedule.duration");
        Map(nameof(SectionSchedule.Cabinet), "schedule.cabinet");
    }
}