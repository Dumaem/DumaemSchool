using MediatR;

namespace DumaemSchool.Core.Notifications;

public sealed record RestorationRequestedNotification(string Email, int RestorationCode) : INotification;