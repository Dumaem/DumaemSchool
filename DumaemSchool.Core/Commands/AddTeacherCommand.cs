using DumaemSchool.Core.Commands.Base;
using DumaemSchool.Core.Models;
using MediatR;

namespace DumaemSchool.Core.Commands;

public sealed record AddTeacherCommand(Teacher Teacher, string Email) : IRequest<Teacher?>, ITransactRequest;