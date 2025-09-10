using OxyzWPF.UI.ViewModels;
using System.Windows.Media;

namespace OxyzWPF.Game;

internal class GameLoop
{
    private DateTime _lastFrameTime;
    private readonly MainViewModel _viewModel;

    public GameLoop(MainViewModel viewModel)
    {
        _viewModel = viewModel;
        _lastFrameTime = DateTime.Now;
        CompositionTarget.Rendering += OnRendering;
    }

    public void OnRendering(object? sender, EventArgs e)
    {
        var currentTime = DateTime.Now;
        var deltaTime = (currentTime - _lastFrameTime).TotalSeconds;
        _lastFrameTime = currentTime;
        _viewModel.Update(deltaTime);
    }
}