using DumaemSchool.Core.Models;
using MediatR;

namespace DumaemSchool.Core.Commands;

public sealed record AddTeacherCommand(Teacher Teacher) : IRequest<Teacher?>;