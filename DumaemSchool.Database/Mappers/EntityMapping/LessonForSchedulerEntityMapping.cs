using DumaemSchool.Core.OutputModels;
using DumaemSchool.Database.Mappers.EntityMapping.Base;

namespace DumaemSchool.Database.Mappers.EntityMapping;

public sealed class LessonForSchedulerEntityMapping : AbstractEntityMapping<LessonForScheduler>
{
    public LessonForSchedulerEntityMapping()
    {
        Map(nameof(LessonForScheduler.LessonId), "l.id", true);
        Map(nameof(LessonForScheduler.TeacherId), "l.teacher_id");
        Map(nameof(LessonForScheduler.SectionGroupName), "section.group_name");
        Map(nameof(LessonForScheduler.LessonStart), "l.date + schedule.time");
        Map(nameof(LessonForScheduler.Duration), "schedule.duration");
        Map(nameof(LessonForScheduler.IsReplacement), "l.teacher_id != ss.teacher_id");
        Map(nameof(LessonForScheduler.IsCancelled), "NOT l.is_conducted");
    }
}