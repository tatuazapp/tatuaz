using System;
using System.Collections.Generic;
using System.Linq;
using FluentValidation;
using FluentValidation.Validators;
using Microsoft.Extensions.DependencyInjection;
using Tatuaz.Shared.Domain.Dtos.Validators.Identity;
using Tatuaz.Shared.Domain.Dtos.Validators.Identity.User;
using Xunit;

namespace Tatuaz.Convention.Test.Shared.Domain.Dtos;

public class ValidatorTest
{
    private readonly IEnumerable<IValidator> validators;

    public ValidatorTest(IServiceProvider serviceProvider)
    {
        validators = typeof(CreateUserDtoValidator).Assembly
            .GetTypes()
            .Where(x => x.IsClass && !x.IsAbstract && x.IsAssignableTo(typeof(IValidator)))
            .Select(x => (IValidator)ActivatorUtilities.CreateInstance(serviceProvider, x))
            .ToList();
    }

    public class NotEmptyValidators : ValidatorTest
    {
        public NotEmptyValidators(IServiceProvider serviceProvider) : base(serviceProvider) { }

        [Fact]
        // We decided that you should use LengthValidator with length > 0
        // instead of NotEmptyValidator as it fulfills the same purpose
        public void ShouldNot_HaveNotEmptyValidator()
        {
            var notEmptyValidators = validators
                .SelectMany(
                    x =>
                        x.CreateDescriptor()
                            .Rules.Select(
                                y =>
                                    (
                                        y.Components,
                                        x.ToString()?.Split(".").Last() + ":" + y.PropertyName
                                    )
                            )
                            .Where(q => q.Components.Any(c => c.Validator is INotEmptyValidator))
                )
                .Select(p => p.Item2);
            Assert.Empty(notEmptyValidators);
        }
    }
}
