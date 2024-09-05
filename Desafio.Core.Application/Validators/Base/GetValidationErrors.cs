using Desafio.Core.Domain.Common;
using FluentValidation.Results;

namespace Desafio.Core.Application.Validators.Base;

public static class GetValidationErrors
{
    public static Response GetErrors(this ValidationResult validationResult)
    {
        var response = new Response();
        if (!validationResult.IsValid)
        {
            foreach (var error in validationResult.Errors)
            {
                response.ReportErrors.Add(new ReportError
                {
                    Code = error.ErrorCode,
                    Message = error.ErrorMessage
                });
            }
        }

        return response;
    }
}