using LiteObservableLanguages;
using System.Globalization;
using System.Windows;
using WpfApp.Windows;

namespace WpfApp;

/// <summary>
/// Interaction logic for App.xaml
/// </summary>
public partial class App : Application
{
    protected override void OnStartup(StartupEventArgs e)
    {
        base.OnStartup(e);

        Locale.Default
            .UseResourceManager(WpfApp.Properties.Resources.ResourceManager)
            .UseFallback(new CultureInfo("en"))
            .UseCulture(CultureInfo.CurrentUICulture);

        new MainWindow().Show();
    }
}
