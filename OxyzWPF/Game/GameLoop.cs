using OxyzWPF.Contracts.ECS;
using OxyzWPF.Contracts.Game;
using OxyzWPF.Contracts.Mailing;
using OxyzWPF.ECS;
using OxyzWPF.UI.ViewModels;
using System.Windows.Media;

namespace OxyzWPF.Game;

internal class GameLoop : IGameLoop
{
    private DateTime _lastFrameTime;
    private readonly MainViewModel _viewModel;
    private readonly ProjectWorld _projectWorld;
    private readonly SupportWorld _supportWorld;
    private IMessenger _messenger;

    public GameLoop(MainViewModel viewModel, ProjectWorld projectWorld, SupportWorld supportWorld, IMessenger messenger)
    {
        _viewModel = viewModel;
        _projectWorld = projectWorld;
        _supportWorld = supportWorld;
        _messenger = messenger;
        _lastFrameTime = DateTime.Now;
        CompositionTarget.Rendering += OnRendering;
    }

    public void OnRendering(object? sender, EventArgs e)
    {
        //Для оптимизации здесь необходимо использовать RenderingTime
        //https://evanl.wordpress.com/2009/12/06/efficient-optimal-per-frame-eventing-in-wpf/
        //var args = (RenderingEventArgs)e;
        //var renderingTime = args.RenderingTime; 
        var currentTime = DateTime.Now;
        var deltaTime = (currentTime - _lastFrameTime).TotalSeconds;
        _lastFrameTime = currentTime;

        // Обновляем ViewModel (для анимации куба)
        _viewModel?.Update(deltaTime);

        // Обновляем ECS мир
        _projectWorld?.Update(deltaTime);
        _supportWorld?.Update(deltaTime);

        _supportWorld?.Clear();

        _messenger?.Update(deltaTime);
    }
}