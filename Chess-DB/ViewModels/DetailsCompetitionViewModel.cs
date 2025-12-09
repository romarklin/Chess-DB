using Chess_DB.Models;
using CommunityToolkit.Mvvm.Input;
using System;
using Avalonia;
using Avalonia.Controls.ApplicationLifetimes;
using Chess_DB.Views;
using System.Threading.Tasks;

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
    private async Task AfficherCoups(Partie partie)
    {
        if (partie == null) return;

        var fenetre = new AfficherCoupsWindow(partie);

        if (Application.Current?.ApplicationLifetime is IClassicDesktopStyleApplicationLifetime desktop)
        {

            if (desktop.MainWindow is not null)
            {
                await fenetre.ShowDialog(desktop.MainWindow);
            }
        }
    }
}