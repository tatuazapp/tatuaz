using System;
using System.Collections.Generic;
using System.Linq;
using Tatuaz.Shared.Domain.Dtos.Dtos.Identity;
using Tatuaz.Shared.Domain.Dtos.Fakers.Identity;
using Xunit;

namespace Tatuaz.Convention.Test.Shared.Domain.Dtos;

public class DtoTest
{
    private readonly List<Type> _dtoFakers;
    private readonly List<Type> _dtos;

    public DtoTest()
    {
        _dtos = typeof(CreateUserDto).Assembly
            .GetTypes()
            .Where(x => x.Name.EndsWith("Dto"))
            .ToList();
        _dtoFakers = typeof(CreateUserDtoFaker).Assembly
            .GetTypes()
            .Where(x => x.Name.EndsWith("Faker"))
            .ToList();

        // TODO add histDtos when created
    }

    public class DtoFakers : DtoTest
    {
        [Fact]
        public void AllDtosHaveFakers()
        {
            var dtosWithoutFakers = _dtos
                .Where(x => !_dtoFakers.Any(y =>
                    y.BaseType?.GenericTypeArguments.FirstOrDefault() == x && y.Name == x.Name + "Faker"))
                .ToList();
            Assert.Empty(dtosWithoutFakers);
        }
    }
}