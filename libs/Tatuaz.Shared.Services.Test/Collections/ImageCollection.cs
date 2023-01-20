using Tatuaz.Shared.Services.Test.Fixtures;
using Xunit;

namespace Tatuaz.Shared.Services.Test.Collections;

[CollectionDefinition("ImageServiceCollection")]
public class ImageCollection : ICollectionFixture<ImageFixture> { }
