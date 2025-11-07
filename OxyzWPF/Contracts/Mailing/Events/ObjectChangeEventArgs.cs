using HelixToolkit.Wpf.SharpDX;

namespace OxyzWPF.Contracts.Mailing.Events;

public sealed class ObjectChangeEventArgs : EventArgs
{

    private readonly MeshGeometryModel3D _geometryModel;

    public ObjectChangeEventArgs(MeshGeometryModel3D geometryModel)
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
