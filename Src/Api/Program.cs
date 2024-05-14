using Api.Settings;
using Application.DI;
using Domain.Services.Jwt.Settings;
using Infra.DI;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.EnableApiVersioning().AddRequestRateLimit();

builder.Services.ApplicationPersist().InfraPersist(builder.Configuration);

builder.Services.AddJwtAuthentication(
    builder.Configuration.GetSection(JwtSettings.Section).Get<JwtSettings>()!
);

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseRateLimiter();

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();
