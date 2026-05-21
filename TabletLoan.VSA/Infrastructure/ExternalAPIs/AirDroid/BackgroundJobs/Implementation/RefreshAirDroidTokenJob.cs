using Microsoft.Extensions.Options;
using TabletLoan.VSA.Infrastructure.ExternalAPIs.AirDroid.DTOs.Request;
using TabletLoan.VSA.Infrastructure.ExternalAPIs.AirDroid.Options;

namespace TabletLoan.VSA.Infrastructure.ExternalAPIs.AirDroid.BackgroundJobs.Implementation;

public class RefreshAirDroidTokenJob(
    IAirDroidAuthClient airDroidAuthClient,
    AirDroidTokenManager tokenManager,
    IOptionsSnapshot<AirDroidOptions> options,
    ILogger<RefreshAirDroidTokenJob> logger
) : IRefreshAirDroidTokenJob
{
    public async Task ExcuteSignInAsync()
    {
        logger.LogInformation("Starting AirDroid Token Refresh...");

        var request = new AirDroidSignInRequest(
                Timestamp: DateTimeOffset.UtcNow.ToUnixTimeSeconds(),
                DeviceType: 75,
                AppVersion: options.Value.AppVersion,
                Language: "es-es",
                Nonce: DateTimeOffset.UtcNow.ToUnixTimeSeconds() + 10,
                ModeType: 101,
                CaptchaType: 1,
                Fp: "38f0c02d57891a602e5d6cd6f2f2f064",
                Mail: options.Value.Mail,
                Pwd: options.Value.Pwd,
                DataRegion: "Global"
            );

        var response = await airDroidAuthClient.SignInAsync(request);

        if (response.Code == 1 && response.Msg == "success")
        {
            tokenManager.SetToken(response.Data.ParentalTokens.Ptoken);
            logger.LogInformation("AirDroid Token Refresh Successful");
        } else if (response.Code == -2)
        {
            logger.LogError("Error Refreshing Token. Wrong Credentials: {Msg}", response.Msg);
        } else
        {
            logger.LogError("Unexpected Error trying to refresh Token. Code: {Code}, Msg: {Msg}", response.Code, response.Msg);
        }
    }
}