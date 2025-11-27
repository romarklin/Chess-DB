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

    public ObservableCollection<Partie> Parties => _competitionService.Parties;

    public string NomCompetition
    {
        get
        {
            // Retourne le nom de la compétition actuelle (ou un texte par défaut si null)
            return _competitionService.CompetitionEnCours?.Nom ?? "Aucune compétition";
        }
        set
        {
            // Met à jour le nom dans le service si la compétition existe
            if (_competitionService.CompetitionEnCours != null)
            {
                if (_competitionService.CompetitionEnCours.Nom != value)
                {
                    _competitionService.CompetitionEnCours.Nom = value;
                    OnPropertyChanged(); // Notifie la vue que la valeur a changé
                }
            }
        }
    }

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

                _competitionService.Sauvegarder();
            }
        }
    }
}