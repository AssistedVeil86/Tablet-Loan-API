using System.Text.Json.Serialization;
using Hangfire;
using Scalar.AspNetCore;
using TabletLoan.VSA.Features.Loans;
using TabletLoan.VSA.Infrastructure.Data;
using TabletLoan.VSA.Infrastructure.Extensions;
using TabletLoan.VSA.Infrastructure.ExternalAPIs.AirDroid;
using TabletLoan.VSA.Infrastructure.ExternalAPIs.AirDroid.BackgroundJobs;
using TabletLoan.VSA.Infrastructure.ExternalAPIs.AirDroid.Options;
using TabletLoan.VSA.Infrastructure.ExternalAPIs.Koha;
using TabletLoan.VSA.Infrastructure.ExternalAPIs.Koha.BackgroundJobs;
using TabletLoan.VSA.Infrastructure.ExternalAPIs.Koha.Options;
using TabletLoan.VSA.Infrastructure.ExternalAPIs.Turnstile;
using TabletLoan.VSA.Infrastructure.Hubs;
using TabletLoan.VSA.Infrastructure.Services;

var builder = WebApplication.CreateBuilder(args);

// Configure JSON Serialization for Numbers
builder.Services.ConfigureHttpJsonOptions(options =>
    options.SerializerOptions.NumberHandling = JsonNumberHandling.Strict);

// Add services to the container.
builder.Services.AddOpenApi();
builder.Services.AddSignalR();

//Add DbContext
builder.Services.AddDbContext<AppDbContext>();

//Configure Infrastructure Services
builder.Services.ConfigureHangFire(builder.Configuration);
builder.Services.ConfigureCors();

builder.Services.AddScoped<KohaStudentService>();
builder.Services.AddScoped<AirDroidTabletService>();
builder.Services.AddScoped<DateTimeService>();

builder.Services.RegisterValidators();

//Add RetroFit Clients
builder.Services.AddTurnstileClient(builder.Configuration);
builder.Services.AddAirDroidClients(builder.Configuration);
builder.Services.AddKohaClient(builder.Configuration);

//Configure Options
builder.Services.AddOptions<AirDroidOptions>()
    .BindConfiguration(AirDroidOptions.SectionName);

builder.Services.AddOptions<KohaOptions>()
    .BindConfiguration(KohaOptions.SectionName);

//Register Singletons
builder.Services.AddSingleton<AirDroidTokenManager>();
builder.Services.AddSingleton<KohaSessionManager>();

//Register External APIs Jobs
builder.Services.RegisterAirDroidAuthJobs();
builder.Services.RegisterKohaJobs();

//Add Features' Handlers and Jobs
builder.Services.RegisterLoanHandlers();
builder.Services.RegisterLoanJobs();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.MapScalarApiReference("scalar", options => options.Layout = ScalarLayout.Classic);
}

using (var scope = app.Services.CreateScope())
{
    var recurringJobManager = scope.ServiceProvider.GetRequiredService<IRecurringJobManager>();

    recurringJobManager.AddOrUpdate<IRefreshAirDroidTokenJob>(
        "airdroid-daily-signin", job => job.ExcuteSignInAsync(), "0 6 * * *");

    recurringJobManager.AddOrUpdate<ISignOutAirDroidJob>(
        "airdroid-daily-signout", job => job.ExecuteSignOutAsync(), "0 20 * * *");

    recurringJobManager.AddOrUpdate<IRefreshKohaSessionJob>(
        "koha-refresh-session", job => job.ExecuteLoginAsync(), Cron.MinuteInterval(1)
    );

    var backgroundJobs = scope.ServiceProvider.GetRequiredService<IBackgroundJobClient>();
    backgroundJobs.Enqueue<IRefreshAirDroidTokenJob>(job => job.ExcuteSignInAsync());
    backgroundJobs.Enqueue<IRefreshKohaSessionJob>(job => job.ExecuteLoginAsync());
}

app.UseHttpsRedirection();
app.UseHangfireDashboard();
app.UseCors("AllowReact");

app.MapLoanEndpoints();
app.MapHub<LoanNotificationHub>("/loanNotifications");
app.MapHub<LoanStatusHub>("/loanStatusNotifications");

app.Run();