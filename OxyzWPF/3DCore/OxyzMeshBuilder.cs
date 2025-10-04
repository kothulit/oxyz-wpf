using SharpDX;
using System.Windows.Media.Media3D;

namespace OxyzWPF._3DCore;

public class OxyzMeshBuilder
{
    private Point3DCollection? positions;
    /// <summary>
    /// Gets the positions collection of the mesh.
    /// </summary>
    /// <value> The positions. </value>
    public Point3DCollection Positions
    {
        get
        {
            return this.positions;
        }
    }

    public void AddAddExtrudedGeometry(IList<Vector3> points, Vector3 xaxis, Vector3 p0, Vector3 p1)
    {
        throw new NotImplementedException();
    }
}
    