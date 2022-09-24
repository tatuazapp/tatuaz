using Tatuaz.Shared.Domain.Models.Common;
using Tatuaz.Shared.Domain.Models.Hist.Common;

namespace Tatuaz.Shared.Domain.Models.Test.Generic;

public class ConventionTest
{
    public class HistIntegration
    {
        private List<Type> _entityTypes;
        private List<Type> _histEntityTypes;
        public HistIntegration()
        {
            _entityTypes = typeof(Entity<,>)
                .Assembly
                .GetTypes()
                .Where(x => typeof(Entity<,>).IsAssignableFrom(x))
                .ToList();
            _histEntityTypes = typeof(HistEntity<>)
                .Assembly
                .GetTypes()
                .Where(x => typeof(HistEntity<>).IsAssignableFrom(x))
                .ToList();
        }
        [Fact]
        public void ShouldHistCounterpartsExistWithCorrectName()
        {
            foreach (var entityType in _entityTypes)
            {
                var value = $"Hist{entityType.Name.Substring(0, entityType.Name.IndexOf('`'))}";
                var present = _histEntityTypes.Any(x => x.Name.Substring(0, x.Name.IndexOf('`')) == value);
                Assert.True(present, $"{entityType.Name} doesn't have a Hist counterpart named {value}");
            }
        }
    }
}
