using OxyzWPF.Contracts.Game;
using OxyzWPF.ECS;
using OxyzWPF.UI.ViewModels;
using System.Windows.Media;

namespace OxyzWPF.Game;

internal class GameLoop : IGameLoop
{
    private DateTime _lastFrameTime;
    private readonly MainViewModel _viewModel;
    private readonly World _world;

    public GameLoop(MainViewModel viewModel, World world)
    {
        _viewModel = viewModel;
        _world = world;
        _lastFrameTime = DateTime.Now;
        CompositionTarget.Rendering += OnRendering;
    }

    public void OnRendering(object? sender, EventArgs e)
    {
        var currentTime = DateTime.Now;
        var deltaTime = (currentTime - _lastFrameTime).TotalSeconds;
        _lastFrameTime = currentTime;

        // Обновляем ViewModel (для анимации куба)
        _viewModel?.Update(deltaTime);

        // Обновляем ECS мир
        _world?.Update(deltaTime);
    }
}