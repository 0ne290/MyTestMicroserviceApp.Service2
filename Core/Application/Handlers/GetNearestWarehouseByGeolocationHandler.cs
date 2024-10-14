using Application.Commands;
using Application.Dtos;
using Application.Mappers;
using Domain.Interfaces;
using FluentResults;
using FluentValidation;
using MediatR;

namespace Application.Handlers;

public class GetNearestWarehouseByGeolocationHandler : IRequestHandler<GetNearestWarehouseByGeolocationCommand, Result<WarehouseDto>>
{
    public GetNearestWarehouseByGeolocationHandler(IWarehouseStorage warehouseStorage, IValidator<GetNearestWarehouseByGeolocationCommand> requestValidator)
    {
        _warehouseStorage = warehouseStorage;
        _requestValidator = requestValidator;
    }

    public async Task<Result<WarehouseDto>> Handle(GetNearestWarehouseByGeolocationCommand request, CancellationToken cancellationToken)
    {
        var validationResult = await _requestValidator.ValidateAsync(request, cancellationToken);

        if (!validationResult.IsValid)
            return Result.Fail(validationResult.ToString(" "));

        var warehouses = await _warehouseStorage.GetAll();
        
        return Result.Ok(WarehouseMapper.EntityToDto(warehouses.MinBy(w => CalculateLengthInKmBetweenTwoPoints(w.Geolocation, (request.GeolocationLongitude, request.GeolocationLatitude)))!));
    }
    
    private static double CalculateLengthInKmBetweenTwoPoints((double Longitude, double Latitude) point1,
        (double Latitude, double Longitude) point2)
    {
        point1.Latitude *= NumberOfRadiansInOneDegree;
        point1.Longitude *= NumberOfRadiansInOneDegree;
        point2.Latitude *= NumberOfRadiansInOneDegree;
        point2.Longitude *= NumberOfRadiansInOneDegree;

        var latitudeCosineOfPoint1 = Math.Cos(point1.Latitude);
        var latitudeCosineOfPoint2 = Math.Cos(point2.Latitude);
        var latitudeSineOfPoint1 = Math.Sin(point1.Latitude);
        var latitudeSineOfPoint2 = Math.Sin(point2.Latitude);
        var longitudeDifference = Math.Abs(point2.Longitude - point1.Longitude);
        var cosineOfLongitudeDifference = Math.Cos(longitudeDifference);

        var angularDifference = Math.Atan2(
            Math.Sqrt(Math.Pow(latitudeCosineOfPoint2 * Math.Sin(longitudeDifference), 2) + Math.Pow(
                latitudeCosineOfPoint1 * latitudeSineOfPoint2 -
                latitudeSineOfPoint1 * latitudeCosineOfPoint2 * cosineOfLongitudeDifference, 2)),
            latitudeSineOfPoint1 * latitudeSineOfPoint2 +
            latitudeCosineOfPoint1 * latitudeCosineOfPoint2 * cosineOfLongitudeDifference);

        return angularDifference * EarthRadiusInKm;
    }

    private const double NumberOfRadiansInOneDegree = Math.PI / 180;

    private const double EarthRadiusInKm = 6_371.0088;

    private readonly IWarehouseStorage _warehouseStorage;

    private readonly IValidator<GetNearestWarehouseByGeolocationCommand> _requestValidator;
}