using Microsoft.AspNetCore.Identity;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace BineshSoloution;

public class AppSettings : IValidatableObject
{
    public IdentitySettings Identity { get; set; } = default!;

    public EmailSettings Email { get; set; } = default!;

    public SmsSettings Sms { get; set; } = default!;
    public DataProtection DataProtection { get; set; } = default!;

    public ShalliDataBaseNameSettings ShalliSettings { get; set; } = default!;

    [Required]
    public string UserProfileImagesDir { get; set; } = default!;

    [Required]
    public string GoogleRecaptchaSecretKey { get; set; } = default!;

    [Required]
    public string OpenAiApiKey { get; set; } = default!;

    [Required]
    public PanelWorkerSettings PanelWorker { get; set; } = default!;

    public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
    {
        var validationResults = new List<ValidationResult>();

        Validator.TryValidateObject(Identity, new ValidationContext(Identity), validationResults, true);
        Validator.TryValidateObject(Email, new ValidationContext(Email), validationResults, true);
        Validator.TryValidateObject(Sms, new ValidationContext(Sms), validationResults, true);

        return validationResults;
    }
}
public class PanelWorkerSettings
{
    public TimeSpan Interval { get; set; } = TimeSpan.FromMinutes(5);
}

public class ShalliDataBaseNameSettings
{
    public AccountName AccountNames { get; set; } = default!;
}

public class AccountName
{
    public string RestoredItemsAccount { get; set; } = default!;
}

public class IdentitySettings : IdentityOptions
{
    public TimeSpan BearerTokenExpiration { get; set; }
    public TimeSpan RefreshTokenExpiration { get; set; }

    [Required]
    public string Issuer { get; set; } = default!;

    [Required]
    public string Audience { get; set; } = default!;

    public TimeSpan EmailTokenRequestResendDelay { get; set; }
    public TimeSpan PhoneNumberTokenRequestResendDelay { get; set; }
    public TimeSpan ResetPasswordTokenRequestResendDelay { get; set; }
    public TimeSpan TwoFactorTokenRequestResendDelay { get; set; }
    public TimeSpan RevokeUserSessionsDelay { get; set; }

    public TimeSpan OtpRequestResendDelay { get; set; }
}

public class EmailSettings
{
    [Required]
    public string Host { get; set; } = default!;
    public bool UseLocalFolderForEmails => Host is "LocalFolder";

    [Range(1, 65535)]
    public int Port { get; set; }
    public string? UserName { get; set; }
    public string? Password { get; set; }

    [Required]
    public string DefaultFromEmail { get; set; } = default!;
    public bool HasCredential => (string.IsNullOrEmpty(UserName) is false) && (string.IsNullOrEmpty(Password) is false);
}

public class SmsSettings
{
    public string? FromPhoneNumber { get; set; }
    public string? TwilioAccountSid { get; set; }
    public string? TwilioAutoToken { get; set; }

    public bool Configured => string.IsNullOrEmpty(FromPhoneNumber) is false &&
                              string.IsNullOrEmpty(TwilioAccountSid) is false &&
                              string.IsNullOrEmpty(TwilioAutoToken) is false;
}

public class DataProtection
{
    public string? DataProtectionCertificatePassword { get; set; }
    public string? DataProtectionCertificatePath { get; set; }
}