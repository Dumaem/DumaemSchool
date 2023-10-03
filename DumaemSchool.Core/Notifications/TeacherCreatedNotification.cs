using DumaemSchool.Core.Models;
using MediatR;

namespace DumaemSchool.Core.Notifications;

public sealed record TeacherCreatedNotification(Teacher Teacher, string Email) : INotification;