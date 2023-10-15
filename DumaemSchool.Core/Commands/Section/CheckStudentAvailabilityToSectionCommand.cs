using DumaemSchool.Core.OutputModels;
using MediatR;

namespace DumaemSchool.Core.Commands.Section;

public sealed record CheckStudentAvailabilityToSectionCommand(int StudentId, List<SectionSchedule> SectionSchedule) : IRequest<bool>;