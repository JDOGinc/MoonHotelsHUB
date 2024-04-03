using MoonhotelsConnectorHub.Application.Services;
using MoonhotelsConnectorHub.Application.UseCase;
using MoonhotelsConnectorHub.Domain.Ports.Outgoing;
using MoonhotelsConnectorHub.Infrastructure.Conectors;
using MoonhotelsConnectorHub.Infrastructure.MapperConfig;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddAutoMapper(typeof(MappingProfile).Assembly);
builder.Services.AddScoped<SearchHotelsUseCase>();
builder.Services.AddScoped<ProviderResponseAggregator>();
builder.Services.AddScoped<IProviderConnector, HotelLegsConnector>();

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
