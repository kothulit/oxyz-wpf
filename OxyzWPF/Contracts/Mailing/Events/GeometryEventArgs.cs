using HelixToolkit.Wpf.SharpDX;

namespace OxyzWPF.Contracts.Mailing.Events;

public sealed class GeometryEventArgs : EventArgs
{
    private readonly MeshGeometryModel3D _geometryModel;

    public GeometryEventArgs(MeshGeometryModel3D geometryModel)
    {
        _geometryModel = geometryModel;
    }

    public MeshGeometryModel3D GeometryModel
    {
        get
        {
            return _geometryModel;
        }
    }
}
