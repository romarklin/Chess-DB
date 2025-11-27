using Chess_DB.Models;
using Chess_DB.Services;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using CommunityToolkit.Mvvm.Input;
using System;
using System.Linq;

namespace Chess_DB.ViewModels;

public partial class InscriptionsViewModel : ViewModelBase
{
    private readonly JoueurService _joueurService;
    private readonly CompetitionService _competitionService;
    private readonly Action _navigateToCompetitions;

    public string Title { get; } = "Page des Inscriptions";

    // Liste des joueurs disponibles (à gauche)
    public ObservableCollection<Joueur> JoueursDisponibles { get; } = new();

    // Liste des joueurs inscrits (à droite)
    public ObservableCollection<Joueur> JoueursInscrits { get; } = new();

    public ObservableCollection<Joueur> Joueurs => _joueurService.Joueurs;

    public InscriptionsViewModel(JoueurService joueurService, CompetitionService competitionService, Action navigateToCompetitions)
    {
        _joueurService = joueurService;
        _competitionService = competitionService;
        _navigateToCompetitions = navigateToCompetitions;

        // 1. Charger les données initiales
        foreach (var joueur in _joueurService.Joueurs)
        {
            JoueursDisponibles.Add(joueur);
        }

        // 2. S'abonner aux changements de la liste principale
        _joueurService.Joueurs.CollectionChanged += OnListePrincipaleChanged;
    }

    // Cette méthode est appelée automatiquement quand la liste principale change
    private void OnListePrincipaleChanged(object? sender, NotifyCollectionChangedEventArgs e)
    {
        // Si des joueurs ont été ajoutés (ex: via la Page Principale)
        if (e.NewItems != null)
        {
            foreach (Joueur nouveauJoueur in e.NewItems)
            {
                // On l'ajoute aux disponibles
                JoueursDisponibles.Add(nouveauJoueur);
            }
        }

        // Si des joueurs ont été supprimés
        if (e.OldItems != null)
        {
            foreach (Joueur ancienJoueur in e.OldItems)
            {
                // On le retire de partout pour éviter les fantômes
                JoueursDisponibles.Remove(ancienJoueur);
                JoueursInscrits.Remove(ancienJoueur);
            }
        }
    }

    [RelayCommand]
    private void Inscrire(Joueur joueur)
    {
        if (joueur == null) return;

        // Déplace le joueur de "Disponibles" vers "Inscrits"
        if (JoueursDisponibles.Contains(joueur))
        {
            JoueursDisponibles.Remove(joueur);
            JoueursInscrits.Add(joueur);
        }
    }

    [RelayCommand]
    private void Desinscrire(Joueur joueur)
    {
        if (joueur != null && JoueursInscrits.Contains(joueur))
        {
            JoueursInscrits.Remove(joueur);     // On retire de la liste des inscrits
            JoueursDisponibles.Add(joueur);     // On remet dans la liste des disponibles
        }
    }

    [RelayCommand]
    private void DemarrerCompetition()
    {
        if (JoueursInscrits.Count < 2) return; // Il faut au moins 2 joueurs

        // 1. Générer les matchs via le service
        _competitionService.DemarrerNouvelleCompetition(JoueursInscrits.ToList(), "Tournoi Local");

        // 2. Changer de page (vers la page compétitions)
        _navigateToCompetitions?.Invoke();
    }

}