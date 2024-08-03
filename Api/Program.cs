using Domain;
using Infrastructure;
using Application;
using Hangfire;
using Hangfire.MemoryStorage;
using Domain.Interfaces;
using Microsoft.Extensions.Options;
using Domain.Options;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddMemoryCache();
builder.Services.AddDomain(builder.Configuration);
builder.Services.AddInfrastructure();
builder.Services.AddApplication();
builder.Services.AddHangfire(config => config.UseMemoryStorage());
builder.Services.AddHangfireServer();
var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.UseHangfireDashboard("/hangfire");

using (var scope = app.Services.CreateScope())
{
    var recurringJobs = scope.ServiceProvider.GetRequiredService<IRecurringJobManager>();
    var options = scope.ServiceProvider.GetRequiredService<IOptions<HangfireOptions>>().Value;

    recurringJobs.AddOrUpdate<IBackgroundJobsService>(
        nameof(IBackgroundJobsService.ProcessDailyDish),
        x => x.ProcessDailyDish(CancellationToken.None),
        options.ProcessDailyDishCron);
}

app.Run();
