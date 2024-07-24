using Serilog.Events;
using Serilog;
using MassTransit;
using Customer.Application.UseCases;
using Customer.Application.Ports;
using Customer.Infrastructure.Db.Repositories;
using Customer.Infrastructure.Services;
using Manufacturer.Application.Consumers;
using Manufacturer.Application.UseCases;
using Manufacturer.Application.Ports;
using Manufacturer.Infrastructure.Db.Repositories;

Log.Logger = new LoggerConfiguration()
    .MinimumLevel.Verbose()
    .MinimumLevel.Override("Microsoft", LogEventLevel.Error)
    .MinimumLevel.Override("MassTransit", LogEventLevel.Information)
    .MinimumLevel.Override("System.Net.Http.HttpClient", LogEventLevel.Warning)
    .Enrich.FromLogContext()
    .Enrich.WithMachineName()
    .WriteTo.Console()
    .CreateLogger();

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddMassTransit(x =>
{
    x.AddConsumer<VehicleOrderCreatedConsumer>();
    x.AddConsumer<ManufactureEngineConsumer>();
    x.AddConsumer<ManufactureChassisConsumer>();
    x.AddConsumer<ManufactureOptionPackConsumer>();

    x.UsingInMemory((context, cfg) =>
    {
        cfg.ConfigureEndpoints(context);
    });
});

builder.Services.AddScoped<CreateVehicleOrder>();
builder.Services.AddScoped<GetMatchingPreassemlbedVehiclesForOrder>();
builder.Services.AddScoped<ReservePreassembledVehicleForPayment>();
builder.Services.AddScoped<OrderVehicle>();
builder.Services.AddScoped<ReservePartsForVehicleOrder>();
builder.Services.AddScoped<IVehicleOrderRepository, VehicleOrderRepository>();
builder.Services.AddScoped<IWarehouseRepository, WarehouseRepository>();
builder.Services.AddScoped<IPaymentService, PaymentService>();

builder.Services.AddScoped<QueueEngineForManufacture>();
builder.Services.AddScoped<QueueChassisForManufacture>();
builder.Services.AddScoped<QueueOptionPackForManufacture>();
builder.Services.AddScoped<IManufactureItemRepo, ManufactureItemRepo>();

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
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
