using System.Threading.Tasks;
using Avalonia.Interactivity;
using Demo.ViewModels;
using DryIoc;
using FluentAvalonia.UI.Windowing;
using TuDog.Bootstrap;
using TuDog.Interfaces.IDialogServers;
using TuDog.Interfaces.MessageBarService;
using TuDog.Interfaces.RegionManagers;
using TuDog.UIs;

namespace Demo;

public partial class MainWindow : TuDogWindow
{
    public MainWindow()
    {
        InitializeComponent();
    }
}