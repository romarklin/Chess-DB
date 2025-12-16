using Avalonia.Controls;
using Chess_DB.Models;
using Chess_DB.ViewModels;

namespace Chess_DB.Views;

public partial class AfficherCoupsWindow : Window
{
    public AfficherCoupsWindow(Partie partie)
    {
        InitializeComponent();
        var vm = new AfficherCoupsViewModel(partie);
        vm.CloseAction = () => this.Close();
        DataContext = vm;
    }

    public AfficherCoupsWindow()
    {
        InitializeComponent();
    }
}