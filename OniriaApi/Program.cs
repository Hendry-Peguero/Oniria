using Oniria.Core.Application.Extensions;
using Oniria.Infrastructure.Identity.Extensions;
using Oniria.Infrastructure.Persistence.Extensions;
using Oniria.Infrastructure.Shared.Extensions;
using OniriaApi.Extensions;



var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddSwaggerExtension();
builder.Services.AddApiVersioningExtension();

//dependency injections
builder.Services.AddPersistenceDependency(builder.Configuration);
builder.Services.AddApplicationDependency();
builder.Services.AddSharedDependency(builder.Configuration);
builder.Services.AddIdentityDependencyApi(builder.Configuration);
builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
builder.Services.AddHttpClient();

builder.Services.AddControllers().AddJsonOptions(options =>
{
    options.JsonSerializerOptions.ReferenceHandler = System.Text.Json.Serialization.ReferenceHandler.Preserve;
});

var app = builder.Build();

await app.AddIdentitySeeds();


// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
