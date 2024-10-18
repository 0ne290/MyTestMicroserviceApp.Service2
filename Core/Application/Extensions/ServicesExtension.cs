using Application.Commands;
using Application.CommandValidators;
using Domain.Interfaces;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Storages.Providers.EntityFramework;
using Storages.Providers.EntityFramework.Implementations;

namespace Application.Extensions;

public static class ServicesExtension
{
    public static IServiceCollection AddApplicationServices(this IServiceCollection services, string connectionString)
    {
        services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(CreateProductCommand).Assembly));
        
        services.AddScoped<IValidator<CreateManufacturerCommand>, CreateManufacturerCommandValidator>();
        services.AddScoped<IValidator<CreateWarehouseCommand>, CreateWarehouseCommandValidator>();
        services.AddScoped<IValidator<CreateProductCommand>, CreateProductCommandValidator>();
        services.AddScoped<IValidator<CreateSupplyCommand>, CreateSupplyCommandValidator>();
        services.AddScoped<IValidator<GetAllProductsByWarehouseGuidCommand>, GetAllProductsByWarehouseGuidCommandValidator>();
        services.AddScoped<IValidator<GetNearestWarehouseByGeolocationCommand>, GetNearestWarehouseByGeolocationCommandValidator>();
        
        services.AddScoped<IManufacturerStorage, ManufacturerStorage>();
        services.AddScoped<IWarehouseStorage, WarehouseStorage>();
        services.AddScoped<IProductStorage, ProductStorage>();
        services.AddScoped<ISupplyStorage, SupplyStorage>();
        
        services.AddDbContext<Service2Context>(options => options.UseSqlite(connectionString));

        var dbContextOptionsBuilder = new DbContextOptionsBuilder<Service2Context>();
        dbContextOptionsBuilder.UseSqlite(connectionString);
        var dbContext = new Service2Context(dbContextOptionsBuilder.Options);
        
        return services;
    }
}