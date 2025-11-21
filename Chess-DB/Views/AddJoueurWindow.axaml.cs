using Avalonia.Controls;
using Chess_DB.ViewModels;

namespace Chess_DB.Views;

public partial class AddJoueurWindow : Window
{
    public AddJoueurWindow()
    {
        InitializeComponent();

        // Quand la fenêtre est ouverte, le DataContext est prêt
        this.Opened += (s, e) =>
        {
            if (DataContext is AddJoueurViewModel vm)
            {
                vm.CloseRequested += () =>
                {
                    this.Close(vm.CreatedJoueur);
                };
            }
        };
    }
}