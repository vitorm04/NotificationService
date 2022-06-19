using MediatR;
using NotificationService.Api;
using NotificationService.Api.Hubs;
using NotificationService.Api.Kafka;
using NotificationService.Api.Kafka.Events;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddLogging(options => options.ClearProviders().AddConsole());
builder.Services.AddMediatR(typeof(Program));
builder.Services.AddSignalR().AddAzureSignalR();
builder.Services.AddScoped<INotificationProducer, NotificationProducer>();
builder.Services.AddTransient<INotificationConsumer, NotificationConsumer>();
builder.Services.AddHostedService<Worker>();
builder.Services.AddControllers();
builder.Services.Configure<KafkaConnectionConfiguration>(builder.Configuration.GetSection("KafkaConnection"));
var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseRouting();
app.UseEndpoints(options =>
{
    options.MapControllers();
    options.MapHub<NotificationHub>("/notification");
});

app.Run();
