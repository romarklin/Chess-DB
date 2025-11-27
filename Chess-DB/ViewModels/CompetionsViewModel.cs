using Chess_DB.Models;
using Chess_DB.Services;
using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.Input;

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

    // Constructeur par défaut (pour le design-time si besoin, optionnel)
    public CompetitionsViewModel() { }

    [RelayCommand]
    private void EncoderResultat(Partie partie)
    {
        // ICI : Logique pour ouvrir une fenêtre d'encodage.
        // Pour l'instant, on simule un résultat aléatoire pour tester le bouton.
        if (partie != null)
        {
            partie.Resultat = "1-0"; // Exemple statique
            // Astuce pour rafraîchir l'affichage si la propriété n'est pas Observable
            // Idéalement, la classe Partie devrait implémenter ObservableObject
        }
    }
}