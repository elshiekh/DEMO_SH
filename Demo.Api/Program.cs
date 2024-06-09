using log4net.Config;
using Microsoft.OpenApi.Models;
using Demo.Infrastructure;

var builder = WebApplication.CreateBuilder(args);
var VL = string.Empty;
//Configure Log4net.
XmlConfigurator.Configure(new FileInfo("log4net.config"));
//Injecting services.
builder.Services.RegisterServices();
// Add services to the container.

// adding caching


builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.AddSecurityDefinition("basic", new OpenApiSecurityScheme
    {
        Description = "api key.",
        Name = "Authorization",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.ApiKey,
        Scheme = "basic"
    });
    c.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "basic"
                },
                In = ParameterLocation.Header
            },
            new List<string>()
        }
    });
});
builder.Services.AddCors(options =>
{
    options.AddPolicy(name: VL,
        builder =>
        {
            builder.WithOrigins("http://localhost:4200")
                //builder.AllowAnyOrigin()
                .AllowAnyMethod()
                .AllowAnyHeader()
                .AllowCredentials();
        });
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseCors(VL);
app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

app.Run();
