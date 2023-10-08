using DumaemSchool.Core.Models;
using MediatR;

namespace DumaemSchool.Core.Queries;

public sealed record SectionTypesQuery : IRequest<IEnumerable<SectionType>>;