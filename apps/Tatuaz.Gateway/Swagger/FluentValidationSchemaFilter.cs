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

/// <summary>
/// Schema filter used to correctly display validation checks in swagger
/// </summary>
public class FluentValidationSchemaFilter : ISchemaFilter
{
    private readonly IServiceProvider _serviceProvider;

    /// <summary>
    /// Default constructor
    /// </summary>
    /// <param name="serviceProvider"></param>
    public FluentValidationSchemaFilter(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
    }

    /// <summary>
    /// Apply the filter to the schema - mark required fields and add validation rules.
    /// Only not null validator results in required flag.
    /// </summary>
    /// <param name="schema"></param>
    /// <param name="context"></param>
    /// <exception cref="InvalidOperationException"></exception>
    public void Apply(OpenApiSchema schema, SchemaFilterContext context)
    {
        var abstractValidatorType = typeof(AbstractValidator<>).MakeGenericType(context.Type);
        var validatorType = new[] { typeof(SignUpDto).Assembly }
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
                    schema.Properties[key].Nullable = false;
                }

                if (ruleComponentValidator.GetType().IsAssignableTo(typeof(ILengthValidator)))
                {
                    var lengthValidator = (ILengthValidator)ruleComponentValidator;

                    if (lengthValidator.Max > 0)
                    {
                        schema.Properties[key].MaxLength = lengthValidator.Max;
                    }

                    if (lengthValidator.Min > 0)
                    {
                        schema.Properties[key].MinLength = lengthValidator.Min;
                    }
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

                if (
                    ruleComponentValidator.GetType().IsAssignableTo(typeof(IComparisonValidator))
                    && decimal.TryParse(
                        ((IComparisonValidator)ruleComponentValidator).ValueToCompare.ToString(),
                        out var value
                    )
                )
                {
                    switch (((IComparisonValidator)ruleComponentValidator).Comparison)
                    {
                        case Comparison.LessThan:
                            schema.Properties[key].ExclusiveMaximum = true;
                            schema.Properties[key].Maximum = value;
                            break;
                        case Comparison.LessThanOrEqual:
                            schema.Properties[key].Maximum = value;
                            break;
                        case Comparison.GreaterThan:
                            schema.Properties[key].ExclusiveMinimum = true;
                            schema.Properties[key].Minimum = value;
                            break;
                        case Comparison.GreaterThanOrEqual:
                            schema.Properties[key].Minimum = value;
                            break;
                        case Comparison.Equal:
                            schema.Properties[key].Minimum = value;
                            schema.Properties[key].Maximum = value;
                            break;
                    }
                }
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
