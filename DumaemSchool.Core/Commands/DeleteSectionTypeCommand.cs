using MediatR;

namespace DumaemSchool.Core.Commands;

public sealed record DeleteSectionTypeCommand(int SectionTypeId) : IRequest<bool>; 