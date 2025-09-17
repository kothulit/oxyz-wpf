using HelixToolkit.Wpf.SharpDX;
using Microsoft.Extensions.DependencyInjection;
using OxyzWPF.ECS;
using OxyzWPF.ECS.Systems;
using OxyzWPF.UI.ViewModels;
using System.Windows;

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

        //Регистрация UI зависимостей
        services.AddSingleton<MainViewModel>();
        services.AddSingleton<MainWindow>();
        services.AddSingleton<Viewport3DX>(s => s.GetRequiredService<MainWindow>().ViewPort);

        //Регистрация ECS
        services.AddSingleton<World>();
        services.AddSingleton<RenderSystem>();


        _serviceProvider = services.BuildServiceProvider();

        _serviceProvider.GetRequiredService<World>().AddSystem(_serviceProvider.GetRequiredService<RenderSystem>());

        var mainWindow = _serviceProvider.GetService<MainWindow>();
        mainWindow.Show();
    }
}