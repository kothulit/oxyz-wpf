using System.Windows.Media.Media3D;
using SharpDX;

namespace OxyzWPF.ECS.Components;

/// <summary>
/// Компонент для позиции, поворота и масштаба объекта
/// </summary>
public class TransformComponent : IComponent
{
    public Vector3 Position { get; set; } = Vector3.Zero;
    public Vector3 Rotation { get; set; } = Vector3.Zero; // В градусах
    public Vector3 Scale { get; set; } = Vector3.One;

    /// <summary>
    /// Получает матрицу трансформации
    /// </summary>
    public Transform3D GetTransform3D()
    {
        var transformGroup = new Transform3DGroup();

        // Позиция
        transformGroup.Children.Add(new TranslateTransform3D(Position.X, Position.Y, Position.Z));

        // Поворот
        transformGroup.Children.Add(new RotateTransform3D(
            new AxisAngleRotation3D(new Vector3D(1, 0, 0), Rotation.X)));
        transformGroup.Children.Add(new RotateTransform3D(
            new AxisAngleRotation3D(new Vector3D(0, 1, 0), Rotation.Y)));
        transformGroup.Children.Add(new RotateTransform3D(
            new AxisAngleRotation3D(new Vector3D(0, 0, 1), Rotation.Z)));

        // Масштаб
        transformGroup.Children.Add(new ScaleTransform3D(Scale.X, Scale.Y, Scale.Z));

        return transformGroup;
    }

    public override string ToString()
    {
        return $"Transform(Pos: {Position}, Rot: {Rotation}, Scale: {Scale})";
    }
}