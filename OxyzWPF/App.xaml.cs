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
using System;

namespace OxyzWPF;

/// <summary>
/// Interaction logic for App.xaml
/// </summary>
public partial class App : Application
{
    private ServiceProvider? _serviceProvider;
    public ServiceProvider? Provider => _serviceProvider;
    
    public App()
    {
        var services = new ServiceCollection();

        services.AddSingleton<IWorld, World>();
        services.AddSingleton<IInstructor, Instructor>();
        services.AddSingleton<MainViewModel>();
        services.AddSingleton<MainWindow>();
        services.AddSingleton<GameStateMachine>();
        services.AddSingleton<IEditorState, StateEdit>();
        services.AddSingleton<IGameLoop, GameLoop>();

        _serviceProvider = services.BuildServiceProvider();
    }

    protected override void OnStartup(StartupEventArgs e)
    {
        base.OnStartup(e);

        var mainViewModel = _serviceProvider.GetRequiredService<MainViewModel>();
        var instructions = _serviceProvider.GetRequiredService<IInstructor>().Instructions;

        mainViewModel.InitialiseToolbarButtons(instructions);

        var mainWindow = _serviceProvider.GetService<MainWindow>();
        mainWindow.Show();
    }
}