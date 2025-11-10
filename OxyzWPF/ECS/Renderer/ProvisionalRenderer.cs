using HelixToolkit.Wpf.SharpDX;
using OxyzWPF._3DCore;
using OxyzWPF.Contracts.Game.States;
using OxyzWPF.Contracts.Mailing;
using OxyzWPF.Contracts.Mailing.Events;
using OxyzWPF.ECS;
using SharpDX;
using System.Windows.Controls;
using System.Windows.Input;

namespace OxyzWPF.ECS.Renderer;

public class ProvisionalRenderer
{
    private readonly IMessenger _messenger;
    private readonly ProvisionalWorld _provisionalWorld;
    public Vector3 _startPoint;
    public Vector3 _endPoint;
    private bool _isArrowRenderEnable = false;
    private int _poinyEntityId = -1;
    private int _arrowEntityId = -1;

    public ProvisionalRenderer(IMessenger messenger, ProvisionalWorld provisionalWorld)
    {
        _messenger = messenger;
        _provisionalWorld = provisionalWorld;

        _messenger.Subscribe<MouseEventArgs>(EventEnum.MouseDown.ToString(), OnMouseDown);
    }

    private void OnMouseDown(object? sender, MouseEventArgs e)
    {
        var isHit = Calculation.GetScreenPointOnZeroPlane(sender, e, out Vector3 _startPoint);
        if (isHit)
        {
            CreateNewPoint();
        }
    }


    public void Update()
    {
    }

    public void CreateNewPoint()
    {
        var pintEntity = Factory.CreatePoint(_provisionalWorld, _startPoint);
    }

}