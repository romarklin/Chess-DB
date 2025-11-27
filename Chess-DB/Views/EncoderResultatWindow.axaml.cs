using Avalonia.Controls;
using Chess_DB.ViewModels;
using Chess_DB.Models;

namespace Chess_DB.Views;

public partial class EncoderResultatWindow : Window
{
    public EncoderResultatWindow(Partie partie)
    {
        InitializeComponent();
        var vm = new EncoderResultatViewModel(partie);

        // On lie l'action de fermeture du VM à la méthode Close() de la fenêtre
        vm.CloseAction = () => this.Close();

        DataContext = vm;
    }

    // Constructeur nécessaire pour le designer
    public EncoderResultatWindow()
    {
        InitializeComponent();
    }
}