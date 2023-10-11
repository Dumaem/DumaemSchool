using MediatR;

namespace DumaemSchool.Core.Commands.SectionType;

public sealed record DeleteSectionTypeCommand(int SectionTypeId) : IRequest<bool>; 