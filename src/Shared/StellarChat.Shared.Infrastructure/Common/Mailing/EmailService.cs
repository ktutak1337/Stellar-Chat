using Microsoft.AspNetCore.Identity;
using StellarChat.Shared.Abstractions.Common.Mailing;
using StellarChat.Shared.Infrastructure.Identity;

namespace StellarChat.Shared.Infrastructure.Common.Mailing;

public class EmailService : IEmailService
{
    private readonly UserManager<ApplicationUser> _userManager;

    public EmailService(UserManager<ApplicationUser> userManager)
    {
        _userManager = userManager;
    }
    public async Task SendPasswordResetEmailAsync(string email, string resetLink)
    {
        string subject = "Reset Password";
        //TODO:M SendPasswordResetEmailAsync
    }

    public async Task SendAccountUnlockEmailAsync(string? email, string callbackUrl)
    {
        var subject = "Unlock your account";
        //TODO:M SendAccountUnlockEmailAsync
    }

    public async Task SendAppointmentCancellationEmailAsync(string email, DateTime appointmentDate, string reason)
    {
        var subject = "Appointment Cancelled";
        //TODO:M SendAppointmentCancellationEmailAsync
    }
}
