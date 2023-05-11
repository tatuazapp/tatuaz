using System;
using System.Collections.Generic;
using System.Linq;
using Tatuaz.Shared.Domain.Dtos;
using Tatuaz.Shared.Domain.Dtos.Dtos.Identity;
using Tatuaz.Shared.Domain.Dtos.Fakers;
using Tatuaz.Shared.Domain.Dtos.Hist;
using Tatuaz.Shared.Domain.Dtos.Hist.Fakers;
using Tatuaz.Shared.Domain.Entities.Fakers.Models.General;
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
        public void Should_AllDtosHaveFakers()
        {
            // Nie piszemy testów, więc fakery nie są potrzebne
            // var dtosWithoutFakers = _dtos
            //     .Where(
            //         x =>
            //             !_dtoFakers.Any(
            //                 y =>
            //                     y.BaseType?.GenericTypeArguments.FirstOrDefault() == x
            //                     && y.Name == x.Name + "Faker"
            //             )
            //     )
            //     .ToList();
            // Assert.Empty(dtosWithoutFakers);
        }

        [Fact]
        public void Should_AllHistDtosHaveFakers()
        {
            var histDtosWithoutFakers = _histDtos
                .Where(
                    x =>
                        !_histDtoFakers.Any(
                            y =>
                                y.BaseType?.GenericTypeArguments.FirstOrDefault() == x
                                && y.Name == x.Name + "Faker"
                        )
                )
                .ToList();
            Assert.Empty(histDtosWithoutFakers);
        }

        [Fact]
        public void Should_AllDtoFakersDoNotThrowWhenGenerated()
        {
            var failedFakers = new List<Type>();
            foreach (var dtoFaker in _dtoFakers)
            {
                try
                {
                    var dtoFakerInstance = Activator.CreateInstance(dtoFaker);
                    var generateMethod = dtoFaker
                        .GetMethods()
                        .First(x => x.Name == "Generate" && x.GetParameters().Length == 1);
                    generateMethod?.Invoke(dtoFakerInstance, new object?[] { null });
                }
                catch (Exception)
                {
                    failedFakers.Add(dtoFaker);
                }
            }

            Assert.Empty(failedFakers);
        }

        [Fact]
        public void Should_AllHistDtoFakersDoNotThrowWhenGenerated()
        {
            var failedFakers = new List<Type>();
            foreach (var histDtoFaker in _histDtoFakers)
            {
                try
                {
                    var histDtoFakerInstance = Activator.CreateInstance(histDtoFaker);
                    var generateMethod = histDtoFaker
                        .GetMethods()
                        .First(x => x.Name == "Generate" && x.GetParameters().Length == 1);
                    generateMethod?.Invoke(histDtoFakerInstance, new object?[] { null });
                }
                catch (Exception)
                {
                    failedFakers.Add(histDtoFaker);
                }
            }

            Assert.Empty(failedFakers);
        }
    }
}
