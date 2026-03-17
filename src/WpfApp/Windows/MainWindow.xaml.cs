using Wpf.Ui.Controls;
using WpfApp.ViewModels;

namespace WpfApp.Windows;

public partial class MainWindow : UiWindow
{
    public MainWindow()
    {
        ExtendsContentIntoTitleBar = true;
        InitializeComponent();
        DataContext = new ViewModel();
    }
}
