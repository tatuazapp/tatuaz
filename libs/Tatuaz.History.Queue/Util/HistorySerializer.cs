using Newtonsoft.Json;
using Tatuaz.History.Queue.Contracts;
using Tatuaz.Shared.Domain.Entities.Hist.Models.Common;
using Tatuaz.Shared.Helpers;

namespace Tatuaz.History.Queue.Util;

public static class HistorySerializer
{
    public static DumpHistoryOrder SerializeDumpHistoryOrder(HistEntity histEntity)
    {
        var jsonSerializer = SerializationUtils.GetTatuazSerializerSettings();
        return new DumpHistoryOrder(
            histEntity.GetType().AssemblyQualifiedName!,
            JsonConvert.SerializeObject(histEntity, jsonSerializer)
        );
    }

    public static HistEntity DeserializeDumpHistoryOrder(DumpHistoryOrder dumpHistoryOrder)
    {
        var type = Type.GetType(dumpHistoryOrder.ObjectType);
        if (type is null)
        {
            throw new InvalidOperationException("Cannot determine type of object to deserialize");
        }

        if (!type.IsSubclassOf(typeof(HistEntity)))
        {
            throw new InvalidOperationException("Cannot deserialize object of type " + type.FullName);
        }

        var jsonSerializer = SerializationUtils.GetTatuazSerializerSettings();

        return (HistEntity)
            JsonConvert.DeserializeObject(dumpHistoryOrder.Object, type, jsonSerializer)!;
    }
}
