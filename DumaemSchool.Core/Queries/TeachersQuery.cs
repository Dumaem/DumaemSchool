using DumaemSchool.Core.DataManipulation;
using DumaemSchool.Core.Models;
using DumaemSchool.Core.OutputModels;
using MediatR;

namespace DumaemSchool.Core.Queries;

public sealed record TeachersQuery(bool IncludeFired, ListParam Params) : IRequest<IEnumerable<TeacherDto>>;