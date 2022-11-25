using System;
using System.Collections.Generic;
using System.Linq;
using Tatuaz.Shared.Domain.Dtos;
using Tatuaz.Shared.Domain.Dtos.Dtos.Identity;
using Tatuaz.Shared.Domain.Dtos.Fakers;
using Tatuaz.Shared.Domain.Dtos.Hist;
using Tatuaz.Shared.Domain.Dtos.Hist.Fakers;
using Xunit;

namespace Tatuaz.Convention.Test.Shared.Domain.Dtos;

public class DtoTest
{
    private readonly IEnumerable<Type> _dtos;
    private readonly IEnumerable<Type> _dtoFakers;
    private readonly IEnumerable<Type> _histDtos;
    private readonly IEnumerable<Type> _histDtoFakers;

    public DtoTest()
    {
        _dtos = DtoHelpers.GetTestableDtoTypes();
        _dtoFakers = DtoFakerHelpers.GetTestableDtoFakerTypes();
        _histDtos = HistDtoHelpers.GetTestableHistDtoTypes();
        _histDtoFakers = HistDtoFakerHelpers.GetTestableHistDtoFakerTypes();
    }

    public class DtoFakers : DtoTest
    {
        [Fact]
        public void AllDtosHaveFakers()
        {
            var dtosWithoutFakers = _dtos
                .Where(
                    x =>
                        !_dtoFakers.Any(
                            y =>
                                y.BaseType?.GenericTypeArguments.FirstOrDefault() == x
                                && y.Name == x.Name + "Faker"
                        )
                )
                .ToList();
            Assert.Empty(dtosWithoutFakers);
        }
    }
}
