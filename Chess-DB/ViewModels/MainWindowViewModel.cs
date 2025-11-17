using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;
using Chess_DB.Models;
using Chess_DB.Services;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace Chess_DB.ViewModels;

public partial class MainWindowViewModel : ObservableObject
{

    private readonly JoueurService _joueurService;

    public ObservableCollection<Joueur> Joueurs => _joueurService.Joueurs;

    [ObservableProperty]
    private Joueur? selectedJoueur;

    partial void OnSelectedJoueurChanged(Joueur? oldValue, Joueur? newValue)
    {
        // Force la réévaluation de CanExecute de la commande
        SupprimerJoueurCommand.NotifyCanExecuteChanged();
    }

    public MainWindowViewModel()
    {
        // Initialisation du service
        _joueurService = new JoueurService();

        // Ajout de quelques joueurs de test
        _joueurService.Ajouter(new Joueur(1, "Ivan", "Ivanovitch", "magnus@chess.com", 2864));
        _joueurService.Ajouter(new Joueur(2, "Naruto", "Uzumaki", "ian@chess.com", 2780));
        _joueurService.Ajouter(new Joueur(3, "Wise", "Worm", "ali@chess.com", 2750));
    }

    [RelayCommand]
    private void TrierParElo()
    {
        var sorted = Joueurs.OrderBy(j => j.ClassementElo).ToList();
        Joueurs.Clear();
        foreach (var j in sorted)
            Joueurs.Add(j);
    }

    [RelayCommand]
    private void TrierParEloDesc()
    {
        var sorted = Joueurs.OrderByDescending(j => j.ClassementElo).ToList();
        Joueurs.Clear();
        foreach (var j in sorted)
            Joueurs.Add(j);
    }

    [RelayCommand]
    private void AjouterJoueur()
    {
        var nouveau = new Joueur { Nom = "Nouveau", Prenom = "Joueur" };
        _joueurService.Ajouter(nouveau);
        SelectedJoueur = nouveau; // sélectionne le joueur ajouté
    }

    [RelayCommand(CanExecute = nameof(CanSupprimer))]
    private void SupprimerJoueur()
    {
        if (SelectedJoueur != null)
        {
            _joueurService.Supprimer(SelectedJoueur);
            SelectedJoueur = null;
        }
    }

    private bool CanSupprimer() => SelectedJoueur != null;
}