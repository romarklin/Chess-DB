using Chess_DB.Models;
using CommunityToolkit.Mvvm.Input;
using System;

namespace Chess_DB.ViewModels;

public partial class JoueurStatsViewModel : ViewModelBase
{
    public Joueur Joueur { get; }
    public string Titre => $"Statistiques de {Joueur.Prenom} {Joueur.Nom}";
    public Action? CloseAction { get; set; }

    // Propriétés calculées pour l'affichage
    public string RatioGagne => Joueur.PartiesJouees > 0
        ? $"{(double)Joueur.Victoires / Joueur.PartiesJouees * 100:F1}%"
        : "0%";

    public JoueurStatsViewModel(Joueur joueur)
    {
        Joueur = joueur;
    }

    public JoueurStatsViewModel()
    {
        Joueur = new Joueur { Nom = "Exemple", Prenom = "Joueur", ClassementElo = 1200 };
    }

    [RelayCommand]
    private void Fermer()
    {
        CloseAction?.Invoke();
    }
}