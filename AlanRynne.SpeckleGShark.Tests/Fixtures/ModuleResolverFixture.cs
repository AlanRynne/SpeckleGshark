using AlanRynne.SpeckleGShark.Modules;

namespace AlanRynne.SpeckleGShark.Tests.Fixtures;

public class ModuleResolverFixture : IDisposable
{
  public GSharkConverterResolver Resolver { get; } = GSharkConverterResolver.BuildDefault();

  public void Dispose()
  {
  }
}