using Microsoft.Extensions.DependencyInjection;
using OxyzWPF.Contracts.ECS;
using OxyzWPF.ECS;
using OxyzWPF.UI.ViewModels;
using System.Windows;

namespace OxyzWPF;

/// <summary>
/// Interaction logic for App.xaml
/// </summary>
public partial class App : Application
{
    private ServiceProvider? _serviceProvider;

    protected override void OnStartup(StartupEventArgs e)
    {
        base.OnStartup(e);

        var services = new ServiceCollection();

        //Регистрация UI зависимостей
        services.AddSingleton<MainViewModel>();
        services.AddSingleton<MainWindow>();

        //Регистрация 
        services.AddSingleton<IWorld, World>();

        _serviceProvider = services.BuildServiceProvider();

        var mainWindow = _serviceProvider.GetService<MainWindow>();
        mainWindow.Show();
    }
}