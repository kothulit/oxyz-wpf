using System.Diagnostics;

namespace OxyzWPF.Editor;

public class NavigationState : IEditorState
{
    public void Enter() => Debug.WriteLine("Navigation mode ON");
    public void Exit() => Debug.WriteLine("Navigation mode OFF");
    public void Update(double deltaTime) { /* Здесь можно обновлять камеру */ }
    public void OnMouseClick(double x, double y)
    {
        Debug.WriteLine($"[Navigation] Click at {x},{y}");
    }
}

public class EditState : IEditorState
{
    public void Enter() => Debug.WriteLine("Edit mode ON");
    public void Exit() => Debug.WriteLine("Edit mode OFF");
    public void Update(double deltaTime) { /* Здесь обновляем выделенные объекты */ }
    public void OnMouseClick(double x, double y)
    {
        Debug.WriteLine($"[Edit] Click at {x},{y}");
    }
}