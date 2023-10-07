using DumaemSchool.Core.Models;
using MediatR;

namespace DumaemSchool.Core.Commands;

public sealed record UpdateSectionTypeNameCommand(SectionType SectionType) : IRequest<bool>;