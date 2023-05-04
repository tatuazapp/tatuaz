using System;
using System.Collections.Generic;

namespace Tatuaz.Shared.Helpers.DataStructures;

public class DisposableList<T> : List<T>, IDisposable
    where T : IDisposable
{
    public void Dispose()
    {
        foreach (var disposable in this)
        {
            disposable.Dispose();
        }
    }
}
