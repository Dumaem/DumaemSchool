using DumaemSchool.Core.OutputModels;

namespace DumaemSchool.Core.Models;

public sealed class SectionWithSchedule
{
    /// <summary>
    /// Название группы студентов
    /// </summary>
    public string GroupName { get; set; } = null!;

    public int SectionTypeId { get; set; }
    public int TeacherId { get; set; }
    public SectionSchedule[] Schedules { get; set; } = null!;
}