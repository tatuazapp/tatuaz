using System;
using System.Linq;
using System.Text;
using FluentValidation;
using FluentValidation.Validators;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;
using Tatuaz.Shared.Domain.Dtos.Dtos.Identity;

namespace Tatuaz.Gateway.Swagger;

public class FluentValidationSchemaFilter : ISchemaFilter
{
    private readonly IServiceProvider _serviceProvider;

    public FluentValidationSchemaFilter(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
    }

    public void Apply(OpenApiSchema schema, SchemaFilterContext context)
    {
        var abstractValidatorType = typeof(AbstractValidator<>).MakeGenericType(context.Type);
        var validatorType = new[] { typeof(CreateUserDto).Assembly }
            .SelectMany(x => x.GetTypes())
            .FirstOrDefault(x => x.IsSubclassOf(abstractValidatorType));
        if (validatorType == null)
        {
            return;
        }

        using var scope = _serviceProvider.CreateScope();
        var validator = scope.ServiceProvider.GetService(validatorType) as IValidator;
        if (validator == null)
        {
            throw new InvalidOperationException(
                "Validator of type " + validatorType + " could not be resolved."
            );
        }

        var validatorDescriptor = validator.CreateDescriptor();
        foreach (var key in schema.Properties.Keys)
        {
            var keyErrorCodes = new StringBuilder("ErrorCodes: ");
            foreach (
                var ruleComponent in validatorDescriptor
                    .GetRulesForMember(ToPascalCase(key))
                    .SelectMany(x => x.Components)
            )
            {
                var ruleComponentValidator = ruleComponent.Validator;
#pragma warning disable CA1305
                keyErrorCodes.Append($"{ruleComponent.ErrorCode}, ");
#pragma warning restore CA1305
                if (ruleComponentValidator.GetType().IsAssignableTo(typeof(INotNullValidator)))
                {
                    schema.Required.Add(key);
                }

                if (ruleComponentValidator.GetType().IsAssignableTo(typeof(INotEmptyValidator)))
                {
                    schema.Properties[key].MinLength = 1;
                }

                if (ruleComponentValidator.GetType().IsAssignableTo(typeof(ILengthValidator)))
                {
                    var lengthValidator = (ILengthValidator)ruleComponentValidator;
                    if (lengthValidator.Max > 0)
                    {
                        schema.Properties[key].MaxLength = lengthValidator.Max;
                    }

                    schema.Properties[key].MinLength = lengthValidator.Min;
                }

                if (
                    ruleComponentValidator
                        .GetType()
                        .IsAssignableTo(typeof(IRegularExpressionValidator))
                )
                {
                    schema.Properties[key].Pattern = (
                        (IRegularExpressionValidator)ruleComponentValidator
                    ).Expression;
                }

                // Add more validation properties here;
            }

            schema.Properties[key].Description = keyErrorCodes.ToString()[
                ..(keyErrorCodes.Length - 2)
            ];
        }
    }

    private static string? ToPascalCase(string? inputString)
    {
        // If there are 0 or 1 characters, just return the string.
        if (inputString == null)
        {
            return null;
        }

        if (inputString.Length < 2)
        {
            return inputString.ToUpper();
        }

        return string.Concat(inputString.Substring(0, 1).ToUpper(), inputString.AsSpan(1));
    }
}
