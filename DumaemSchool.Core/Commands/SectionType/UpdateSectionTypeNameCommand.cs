using MediatR;

namespace DumaemSchool.Core.Commands.SectionType;

public sealed record UpdateSectionTypeNameCommand(Models.SectionType SectionType) : IRequest<bool>;