namespace StellarChat.Shared.Abstractions.Common.Mailing;

public interface IEmailService
{
    Task SendAccountUnlockEmailAsync(string? email, string callbackUrl);
    Task SendPasswordResetEmailAsync(string email, string resetLink);
    Task SendAppointmentCancellationEmailAsync(string email, DateTime appointmentDate, string reason);
}
