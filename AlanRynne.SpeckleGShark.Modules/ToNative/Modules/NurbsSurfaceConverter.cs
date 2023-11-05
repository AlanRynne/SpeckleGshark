using AlanRynne.SpeckleGShark.Core.Context;
using AlanRynne.SpeckleGShark.Core.Converters;
using AlanRynne.SpeckleGShark.Core.Interfaces;
using Microsoft.Extensions.Logging;

namespace AlanRynne.SpeckleGShark.Modules.ToNative.Modules;

public class NurbsSurfaceConverter : ConverterBase<NurbsSurfaceConverter, OG.Surface, GSG.NurbsSurface>
{
  private readonly IObjectConverter<OG.Point, GSG.Point3> pointConverter;

  public NurbsSurfaceConverter(ILogger<NurbsSurfaceConverter> logger,
                               IConverterContextProvider<GSharkConverterContext> context,
                               IObjectConverter<OG.Point, GSG.Point3> pointConverter) :
    base(logger)
  {
    this.pointConverter = pointConverter;
  }

  protected override GSG.NurbsSurface PerformConversion(OG.Surface obj)
  {
    var controlPoints = obj.GetControlPoints();
    var points = controlPoints.Select(group => group.Select(pointConverter.Convert).ToList()).ToList();
    var weights = controlPoints.Select(group => group.Select(pt => pt.weight).ToList()).ToList();

    // TODO: This is lossy, as it's missing the knotVector to initialize with. Issue will be raised on GShark.
    var srf = GSG.NurbsSurface.FromPoints(obj.degreeU, obj.degreeV, points, weights);
    return srf;
  }
}