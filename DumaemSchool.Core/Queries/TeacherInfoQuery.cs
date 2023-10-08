using DumaemSchool.Core.Models;
using MediatR;

namespace DumaemSchool.Core.Queries;

public sealed record TeacherInfoQuery(int TeacherId) : IRequest<Teacher?>;