using System.Linq;

using Ardalis.Result;
using Microsoft.AspNetCore.Mvc;
using InTouch.UserService.Models;


namespace InTouch.UserService.Extensions;

internal static class ResultExtensions
{
    public static IActionResult ToActionResult (this Result result) =>
        result.IsSuccess
            ? new OkObjectResult(ApiResponse.Ok(result.SuccessMessage))
            : result.ToHttpNonSuccessResult();

    public static IActionResult ToActionResult<T>(this Result<T> result) =>
        result.IsSuccess
            ? new OkObjectResult(ApiResponse<T>.Ok(result.Value, result.SuccessMessage))
            : result.ToHttpNonSuccessResult();

    private static IActionResult ToHttpNonSuccessResult(this IResult result)
    {
        var errors = result.Errors.Select(
            error => new ApiErrorResponse(error)).ToList();
        
        switch (result.Status)
        {
            case ResultStatus.Invalid:
                var validationErrors = result
                    .ValidationErrors
                    .Select(validation => new ApiErrorResponse(validation.ErrorMessage));
                
                return new BadRequestObjectResult(ApiResponse.BadRequest(validationErrors));
            
            case ResultStatus.NotFound:
                return new NotFoundObjectResult(ApiResponse.NotFound(errors));
            
            case ResultStatus.Forbidden:
                return new ForbidResult();
            
            case ResultStatus.Unauthorized:
                return new UnauthorizedObjectResult(ApiResponse.Unauthorized(errors));
            
            default:
                return new BadRequestObjectResult(ApiResponse.BadRequest(errors));
        }
    }
}