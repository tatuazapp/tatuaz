using System;
using System.IO;

namespace Tatuaz.Dashboard.Queue.Contracts.Blob;

public record AddBlobFile(Guid Id, byte[] Data);
