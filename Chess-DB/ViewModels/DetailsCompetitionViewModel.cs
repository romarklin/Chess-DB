using Chess_DB.Models;
using CommunityToolkit.Mvvm.Input;
using System;
using Avalonia; // Pour Application
using Avalonia.Controls.ApplicationLifetimes;
using Chess_DB.Views; // Pour AfficherCoupsWindow

namespace Chess_DB.ViewModels;

public partial class DetailsCompetitionViewModel : ViewModelBase
{
    public Competition Competition { get; }

    private readonly Action _retourAction;

    public string Title => $"Détails : {Competition.Nom} ({Competition.DateDebut:dd/MM/yyyy})";

    // Constructeur
    public DetailsCompetitionViewModel(Competition competition, Action retourAction)
    {
        Competition = competition;
        _retourAction = retourAction;
    }

    // Constructeur vide pour le designer
    public DetailsCompetitionViewModel()
    {
        Competition = new Competition();
        _retourAction = () => { };
    }

    [RelayCommand]
    private void Retour()
    {
        _retourAction?.Invoke();
    }

    [RelayCommand]
    private void AfficherCoups(Partie partie)
    {
        if (partie == null) return;

        var fenetre = new AfficherCoupsWindow(partie);

        if (Application.Current?.ApplicationLifetime is IClassicDesktopStyleApplicationLifetime desktop)
        {
            // On l'ouvre en mode Dialog (bloquant) ou Show (non bloquant), au choix.
            // ShowDialog est souvent mieux pour une popup d'info.
            fenetre.ShowDialog(desktop.MainWindow);
        }
    }
}