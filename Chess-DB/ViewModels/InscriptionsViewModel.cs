using Chess_DB.Models;
using Chess_DB.Services;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace Chess_DB.ViewModels;

public partial class InscriptionsViewModel : ViewModelBase
{
    private readonly JoueurService _joueurService;

    public string Title { get; } = "Page des Inscriptions";

    // Liste des joueurs disponibles (à gauche)
    public ObservableCollection<Joueur> JoueursDisponibles { get; } = new();

    // Liste des joueurs inscrits (à droite)
    public ObservableCollection<Joueur> JoueursInscrits { get; } = new();

    public ObservableCollection<Joueur> Joueurs => _joueurService.Joueurs;

    public InscriptionsViewModel(JoueurService joueurService)
    {
        _joueurService = joueurService;

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

}