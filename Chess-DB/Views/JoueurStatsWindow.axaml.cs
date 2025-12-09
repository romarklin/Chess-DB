using Avalonia.Controls;
using Chess_DB.Models;
using Chess_DB.ViewModels;

namespace Chess_DB.Views;

public partial class JoueurStatsWindow : Window
{
    public JoueurStatsWindow(Joueur joueur)
    {
        InitializeComponent();
        var vm = new JoueurStatsViewModel(joueur);
        vm.CloseAction = () => this.Close();
        DataContext = vm;
    }

    public JoueurStatsWindow()
    {
        InitializeComponent();
    }
}