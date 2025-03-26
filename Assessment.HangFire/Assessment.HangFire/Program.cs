using Hangfire;
using Hangfire.SqlServer;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

string connectionString = builder.Configuration.GetConnectionString("DefaultConnection");


builder.Services.AddHangfire(config => config
    .SetDataCompatibilityLevel(CompatibilityLevel.Version_170)
    .UseSimpleAssemblyNameTypeSerializer()
    .UseRecommendedSerializerSettings()
    .UseSqlServerStorage(connectionString, new SqlServerStorageOptions
    {
        CommandBatchMaxTimeout = TimeSpan.FromMinutes(5),
        SlidingInvisibilityTimeout = TimeSpan.FromMinutes(5),
        QueuePollInterval = TimeSpan.Zero, // Real-time processing
        UseRecommendedIsolationLevel = true,
        DisableGlobalLocks = true // Recommended for performance
    }));
builder.Services.AddTransient<PayrollService>();




var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseHangfireDashboard("/mydashboard");

BackgroundJob.Enqueue<PayrollService>(x => x.SendPayrollReportsAsync());

// Schedule a recurring job (e.g., every month)
RecurringJob.AddOrUpdate<PayrollService>("payroll-report", x => x.SendPayrollReportsAsync(), Cron.Monthly);

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
