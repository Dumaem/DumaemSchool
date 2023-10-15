using MediatR;

namespace DumaemSchool.Core.Commands.Section;

public sealed record CreateSectionCommand(Models.SectionWithSchedule Section) : IRequest<Models.Section>;