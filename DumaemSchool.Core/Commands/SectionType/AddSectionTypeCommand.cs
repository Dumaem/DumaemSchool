using MediatR;

namespace DumaemSchool.Core.Commands.SectionType;

public sealed record AddSectionTypeCommand(string SectionTypeName) : IRequest<int>;