using Microsoft.Extensions.DependencyInjection;
using OxyzWPF.Contracts.ECS;
using OxyzWPF.Contracts.Game;
using OxyzWPF.Contracts.Instruction;
using OxyzWPF.ECS;
using OxyzWPF.Game;
using OxyzWPF.Game.States;
using OxyzWPF.UI.ViewModels;
using OxyzWPF.Editor;
using System.Windows;
using OxyzWPF.Contracts.Game.States;
using OxyzWPF.EventBus;
using OxyzWPF.Contracts.Mailing;
using OxyzWPF.Mailing;
using OxyzWPF.ECS.Systems;
using OxyzWPF.Transponder;
using OxyzWPF.Contracts.Transponder;

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

        services.AddSingleton<IWorld, RealWorld>();
        services.AddSingleton<IMailer, Mailer>();
        services.AddSingleton<IInstructor, Instructor>();
        services.AddSingleton<IInputTransponder, InputTransponder>();
        services.AddSingleton<MainViewModel>();
        services.AddSingleton<MainWindow>();
        services.AddSingleton<IGameStateMachine, GameStateMachine>();
        services.AddSingleton<IGameState, StateEdit>();
        services.AddSingleton<IGameLoop, GameLoop>();
        services.AddSingleton<RenderSystem>();

        _serviceProvider = services.BuildServiceProvider();
    }

    protected override void OnStartup(StartupEventArgs e)
    {
        base.OnStartup(e);

        
        var realWorld = _serviceProvider.GetService<IWorld>();
        var gameStateMachine = _serviceProvider.GetService<IGameStateMachine>();
        realWorld.AddSystem(_serviceProvider.GetRequiredService<RenderSystem>());

        var mainViewModel = _serviceProvider.GetRequiredService<MainViewModel>();
        var instructions = _serviceProvider.GetRequiredService<IInstructor>().Instructions;

        mainViewModel.InitialiseToolbarButtons(instructions);

        var mainWindow = _serviceProvider.GetService<MainWindow>();
        mainWindow.Show();

        var gameLoop = _serviceProvider.GetRequiredService<IGameLoop>();
    }
}