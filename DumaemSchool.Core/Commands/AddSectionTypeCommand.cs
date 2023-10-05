using MediatR;

namespace DumaemSchool.Core.Commands;

public sealed record AddSectionTypeCommand(string SectionTypeName) : IRequest<int>;