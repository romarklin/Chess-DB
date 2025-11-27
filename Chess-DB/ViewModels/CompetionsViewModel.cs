using Chess_DB.Models;
using Chess_DB.Views;
using Chess_DB.Services;
using System.Collections.ObjectModel;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia;
using CommunityToolkit.Mvvm.Input;
using System.Threading.Tasks;

namespace Chess_DB.ViewModels;

public partial class CompetitionsViewModel : ViewModelBase
{
    private readonly CompetitionService _competitionService;

    public string Title { get; } = "Liste des matchs";

    // Liaison directe vers la liste du service
    public ObservableCollection<Partie> Parties => _competitionService.Parties;

    public CompetitionsViewModel(CompetitionService competitionService)
    {
        _competitionService = competitionService;
    }

    [RelayCommand]
    private async Task EncoderResultat(Partie partie)
    {
        if (partie == null) return;

        var fenetreEncodage = new EncoderResultatWindow(partie);

        if (Application.Current?.ApplicationLifetime is IClassicDesktopStyleApplicationLifetime desktop)
        {
            if (desktop.MainWindow is not null)
            {
                await fenetreEncodage.ShowDialog(desktop.MainWindow);
            }
        }
    }
}