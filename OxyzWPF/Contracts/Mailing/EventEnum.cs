namespace OxyzWPF.Contracts.Mailing;

public enum EventEnum
{
    TestEvent = 0, // TestEventEventArgs
    KeyPress = 1, // System.Windows.Input.KeyEventArgs 
    MouseDown = 2, // MouseOxyzEventArgs
    ElementAdded = 3, // GeometryEventArgs
    ElementRemoved = 4, // GeometryEventArgs
    GameStateChanged = 5, // GameStateEventArgs
    GameStateChangeRequest = 6, // GameStateChangeRequestEventArgsy
    InstructionStart = 7, // InstructionEventArgs
    InstructionCanseled = 8, // EventArgs
    SelectionChange = 9, // SelectionChangeEventArgs
    HitToGeometryModel = 10, // GeometryChangeEventArgs
    MouseMove = 11, // System.Windows.Input.MouseEventArgs
}
