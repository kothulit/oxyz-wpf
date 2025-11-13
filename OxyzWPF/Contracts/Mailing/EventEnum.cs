namespace OxyzWPF.Contracts.Mailing;

/// <summary>
/// Перечисление дефолтных событий. Для методов месенджера использовать с ToString. В описании значений приводятся типы арнументов хендлера.
/// </summary>
public enum EventEnum
{
    /// <summary>
    /// StatusEventArgs - тип аргументов события.
    /// </summary>
    StatusChangedEvent = 0,
    /// <summary>
    /// KeyEventArgs - тип аргументов события.
    /// </summary>
    KeyPress = 1,
    /// <summary>
    /// MouseMovedEventArgs - тип аргументов события.
    /// </summary>
    MouseDown = 2,
    /// <summary>
    /// GeometryEventArgs - тип аргументов события.
    /// </summary>
    ElementAdded = 3,
    /// <summary>
    /// GeometryEventArgs - тип аргументов события.
    /// </summary>
    ElementRemoved = 4,
    /// <summary>
    /// GameStateEventArgs - тип аргументов события.
    /// </summary>
    GameStateChanged = 5,
    /// <summary>
    /// GameStateChangeRequestEventArgsy - тип аргументов события.
    /// </summary>
    GameStateChangeRequest = 6,
    /// <summary>
    /// InstructionEventArgs - тип аргументов события.
    /// </summary>
    InstructionStart = 7,
    /// <summary>
    /// EventArgs - тип аргументов события.
    /// </summary>
    Сancellation = 8,
    /// <summary>
    /// SelectionChangeEventArgs - тип аргументов события.
    /// </summary>
    SelectionChange = 9,
    /// <summary>
    /// GeometryChangeEventArgs - тип аргументов события.
    /// </summary>
    HitToGeometryModel = 10,
    /// <summary>
    /// MouseEventArgs - тип аргументов события.
    /// </summary>
    MouseMove = 11,
}
