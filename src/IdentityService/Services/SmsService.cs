using Microsoft.Extensions.Options;
using Twilio.Rest.Api.V2010.Account;

namespace DataBaseManager.Services;

public partial class SmsService 
{
    private readonly IOptions<AppSettings> _appSettings = default!;
    private readonly ILogger<SmsService> _logger = default!;
    private readonly IHostEnvironment _hostEnvironment = default!;

    public SmsService(IOptions<AppSettings> appSettings , ILogger<SmsService> logger , IHostEnvironment hostEnvironment)
    {
        _appSettings = appSettings;
        _logger = logger;
        _hostEnvironment = hostEnvironment;
    }

    public async Task SendSms(string messageText, string phoneNumber, CancellationToken cancellationToken)
    {
        if (_hostEnvironment.IsDevelopment())
        {
            LogSendSms(_logger, messageText, phoneNumber);
        }

        if (_appSettings.Value.Sms.Configured is false) return;

        var messageOptions = new CreateMessageOptions(new(phoneNumber))
        {
            From = new(_appSettings.Value.Sms.FromPhoneNumber),
            Body = messageText
        };

        var smsMessage = MessageResource.Create(messageOptions);

        if (smsMessage.ErrorCode is null) return;

        LogSendSmsFailed(_logger, phoneNumber, smsMessage.ErrorCode, smsMessage.ErrorMessage);
    }

    [LoggerMessage(Level = LogLevel.Information, Message = "SMS: {message} to {phoneNumber}.")]
    private static partial void LogSendSms(ILogger logger, string message, string phoneNumber);

    [LoggerMessage(Level = LogLevel.Error, Message = "Failed to send Sms to {phoneNumber}. Code: {errorCode}, Error message: {errorMessage}")]
    private static partial void LogSendSmsFailed(ILogger logger, string phoneNumber, int? errorCode, string errorMessage);
}
