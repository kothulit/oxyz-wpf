namespace OxyzWPF.Messenger;

public enum MessengerEvents
{
    //BasePointController
    BasePointTransformChanged, //Transform
                               //CameraController
    CameraModeChanged, //CameraMode
                       //InputManager
    LeftMouseDown, //Vector3
    RightMouseDown, //Vector3
    MiddleMouseDown, //Vector3
    LeftMouseUp, //Vector3
    LeftMouseDrag, //Vector3
    RightMouseDrag, //Vector3
    MiddleMouseDrag, //Vector3
    MouseScroll, //float
    KeyDown, //KeyCode
    MouseMove, //Vector3
               //UIManager
    SelectObject, //SelectableObject
    ClearSelection, //None
    SelectionChanged, //List<GameObject>
    OnUIModeChanged, //UIMode
                     //CommandPanelController
    CommandFromPanelInvoked //
}
