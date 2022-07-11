using ftalive_webapi.Config;
using Microsoft.AspNetCore.Cors.Infrastructure;

var MyAllowSpecificOrigins = "_myAllowSpecificOrigins";

var builder = WebApplication.CreateBuilder(args);
IConfiguration configuration=null;

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddCors(options =>
{
    OriginConfig oc = new OriginConfig();
    configuration?.GetSection("OriginsConfig")?.Bind(oc);
    options.AddPolicy(name: MyAllowSpecificOrigins,
                      policy =>
                      {
                          if (oc.AllowAll)
                          {
                              policy.AllowAnyOrigin();
                          }
                          else
                          {
                              policy.WithOrigins(oc.AllowedOrigins);
                          }
                      });
});

var app = builder.Build();
configuration = app.Configuration;

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseCors(MyAllowSpecificOrigins);

app.UseAuthorization();

app.MapControllers();

app.Run();
