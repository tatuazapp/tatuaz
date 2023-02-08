using System.Collections.Generic;
using Moq;
using Moq.EntityFrameworkCore;
using Tatuaz.Shared.Domain.Entities.Models.Identity;
using Tatuaz.Shared.Infrastructure.DataAccess;

namespace Tatuaz.Testing.Mocks.Infrastructure;

public class MainDbContextMock : Mock<MainDbContext>
{
    public MainDbContextMock()
    {
        TatuazUsers = new List<TatuazUser>();
        TatuazRoles = new List<TatuazRole>();
        TatuazUserRoles = new List<TatuazUserRole>();

        Setup(x => x.TatuazUsers).ReturnsDbSet(TatuazUsers);
        Setup(x => x.TatuazRoles).ReturnsDbSet(TatuazRoles);
        Setup(x => x.TatuazUserRoles).ReturnsDbSet(TatuazUserRoles);
        Setup(x => x.Set<TatuazUser>()).ReturnsDbSet(TatuazUsers);
        Setup(x => x.Set<TatuazRole>()).ReturnsDbSet(TatuazRoles);
        Setup(x => x.Set<TatuazUserRole>()).ReturnsDbSet(TatuazUserRoles);
        Setup(x => x.Add(It.IsAny<TatuazUser>())).Callback<TatuazUser>(TatuazUsers.Add);
        Setup(x => x.Add(It.IsAny<TatuazRole>())).Callback<TatuazRole>(TatuazRoles.Add);
        Setup(x => x.Add(It.IsAny<TatuazUserRole>())).Callback<TatuazUserRole>(TatuazUserRoles.Add);
        Setup(x => x.Remove(It.IsAny<TatuazUser>()))
            .Callback<TatuazUser>(x => TatuazUsers.Remove(x));
        Setup(x => x.Remove(It.IsAny<TatuazRole>()))
            .Callback<TatuazRole>(x => TatuazRoles.Remove(x));
        Setup(x => x.Remove(It.IsAny<TatuazUserRole>()))
            .Callback<TatuazUserRole>(x => TatuazUserRoles.Remove(x));
    }

    public List<TatuazUser> TatuazUsers { get; set; }
    public List<TatuazRole> TatuazRoles { get; set; }
    public List<TatuazUserRole> TatuazUserRoles { get; set; }
}
