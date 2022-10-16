using Tatuaz.Shared.Domain.Entities.Common;
using Tatuaz.Shared.Domain.Entities.Hist.Common;

namespace Tatuaz.Convention.Test.Shared.Domain.Entities;

public class ConventionTest
{
    public class HistIntegration
    {
        private readonly List<Type> _entityTypes;
        private readonly List<Type> _histEntityTypes;

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
