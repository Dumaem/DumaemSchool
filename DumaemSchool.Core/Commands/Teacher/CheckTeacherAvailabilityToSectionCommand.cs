using DumaemSchool.Core.OutputModels;
using MediatR;

namespace DumaemSchool.Core.Commands.Teacher;

public sealed record CheckTeacherAvailabilityToSectionCommand(int TeacherId, List<SectionSchedule> SectionSchedule) : IRequest<bool>;