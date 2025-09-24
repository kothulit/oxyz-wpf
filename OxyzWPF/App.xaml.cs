using HelixToolkit.Wpf.SharpDX;
using Microsoft.Extensions.DependencyInjection;
using OxyzWPF.Contracts.ECS;
using OxyzWPF.Contracts.Game;
using OxyzWPF.Contracts.Editor;
using OxyzWPF.ECS;
using OxyzWPF.ECS.Systems;
using OxyzWPF.Game;
using OxyzWPF.Game.States;
using OxyzWPF.UI.ViewModels;
using OxyzWPF.Editor;
using System.Windows;
using OxyzWPF.Contracts.Game.States;

namespace OxyzWPF;

/// <summary>
/// Interaction logic for App.xaml
/// </summary>
public partial class App : Application
{
    private ServiceProvider? _serviceProvider;
    public ServiceProvider? Provider => _serviceProvider;

    protected override void OnStartup(StartupEventArgs e)
    {
        base.OnStartup(e);

        var services = new ServiceCollection();
        //Регистрация ECS
        services.AddSingleton<IWorld, World>();

        //Регистрация UI зависимостей
        services.AddSingleton<MainViewModel>();
        services.AddSingleton<MainWindow>();
        services.AddSingleton(s => s.GetRequiredService<MainWindow>().ViewPort);

        //Регистрация инструкций
        services.AddSingleton<IInstructor, Instructor>();

        //Состояния игры
        services.AddSingleton<GameStateMachine>();

        services.AddSingleton<IEditorState, StateEdit>();

        //Регистрация игрового цикла
        services.AddSingleton<IGameLoop, GameLoop>();

        _serviceProvider = services.BuildServiceProvider();

        //_serviceProvider.GetRequiredService<IWorld>().AddSystem(_serviceProvider.GetRequiredService<RenderSystem>());


        var mainWindow = _serviceProvider.GetService<MainWindow>();
        mainWindow.Show();
    }
}