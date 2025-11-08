using HelixToolkit.Wpf.SharpDX;
using OxyzWPF._3DCore;
using OxyzWPF.Contracts.ECS;
using OxyzWPF.Contracts.Game.States;
using OxyzWPF.Contracts.Mailing;
using OxyzWPF.Contracts.Mailing.Events;
using OxyzWPF.ECS;
using OxyzWPF.ECS.Components;
using SharpDX;

namespace OxyzWPF.UI.Renderer;

public class ProvisionalRenderer
{
    private readonly IMessenger _messenger;
    private readonly ProvisionalWorld _provisionalWorld;
    public Vector2 _currentMousePosition;
    public Viewport _viewport;
    private bool _isPointRenderEnable = false;
    private bool _isArrowRenderEnable = false;
    private int _poinyEntityId = -1;
    private int _arrowEntityId = -1;

    public ProvisionalRenderer(IMessenger messenger, ProvisionalWorld provisionalWorld)
    {
        _messenger = messenger;
        _provisionalWorld = provisionalWorld;

        _messenger.Subscribe<GameStateEventArgs>(EventEnum.GameStateChanged.ToString(), OnGameStatusChanged);
    }

    private void OnGameStatusChanged(object sender, GameStateEventArgs e)
    {
        IGameState state = e.CurrentState;
        _provisionalWorld.Clear();
        if (_poinyEntityId != -1) _provisionalWorld.RemoveEntity(_poinyEntityId);
        if (_arrowEntityId != -1) _provisionalWorld.RemoveEntity(_arrowEntityId);

        switch (state.StateName)
        {
            case "Add":
                _isPointRenderEnable = true;
                _isArrowRenderEnable = false;
                break;
            case "Edit":
                _isPointRenderEnable = true;
                _isArrowRenderEnable = true;
                break;
        }
    }

    public void Update()
    {
        if (_isPointRenderEnable)
        {
            //Calculation.GetScreenPointOnZeroPlane()
            //Factory.CreatePoint(_provisionalWorld, )
        }
    }

    public void CreateNewPoint()
    {
        //_currentMousePosition

        //var pintEntity = Factory.CreatePoint(_provisionalWorld, )
    }

}