using FluentResults;

namespace Application.Extensions;

public static class ResultExtension
{
    public static string ErrorsToJson(this Result result) =>
        $"{{\n\t\"errorMessages\": \"{string.Join(" ", result.Errors.Select(e => e.Message))}\"\n}}";
}