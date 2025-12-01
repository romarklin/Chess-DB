using Chess_DB.Models;
using CommunityToolkit.Mvvm.Input;
using System;

namespace Chess_DB.ViewModels;

public partial class AfficherCoupsViewModel : ViewModelBase
{
    public string Titre { get; }
    public string TexteCoups { get; }

    // Action pour fermer la fenêtre
    public Action? CloseAction { get; set; }

    public AfficherCoupsViewModel(Partie partie)
    {
        Titre = $"{partie.JoueurBlanc.Nom} vs {partie.JoueurNoir.Nom}";

        // On affiche un message si aucun coup n'est enregistré
        TexteCoups = string.IsNullOrWhiteSpace(partie.Coups)
            ? "Aucun coup n'a été enregistré pour ce match."
            : partie.Coups;
    }

    // Constructeur vide pour le designer
    public AfficherCoupsViewModel()
    {
        Titre = "Exemple vs Exemple";
        TexteCoups = "1. e4 e5";
    }

    [RelayCommand]
    private void Fermer()
    {
        CloseAction?.Invoke();
    }
}