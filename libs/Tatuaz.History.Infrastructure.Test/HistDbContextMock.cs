using Moq;
using Moq.EntityFrameworkCore;
using Tatuaz.History.DataAccess.Test.Utils;

namespace Tatuaz.History.DataAccess.Test;

public class HistDbContextMock : Mock<HistDbContext>
{
    public HistDbContextMock()
    {
        TestHistEntities = new List<TestHistEntity>();
        Setup(x => x.Set<TestHistEntity>()).ReturnsDbSet(TestHistEntities);
        Setup(x => x.Add(It.IsAny<TestHistEntity>())).Callback(TestHistEntities.Add);
    }

    public List<TestHistEntity> TestHistEntities { get; set; }
}
