using System.Collections.ObjectModel;
using System.Linq;
using Chess_DB.Models;
using Chess_DB.Views;
using Chess_DB.Services;
using CommunityToolkit.Mvvm.ComponentModel;
using Avalonia.Controls.ApplicationLifetimes;
using CommunityToolkit.Mvvm.Input;
using Avalonia;
using System.Threading.Tasks;

namespace Chess_DB.ViewModels;

public partial class PagePrincipaleViewModel : ViewModelBase
{
    private readonly JoueurService _joueurService;

    public ObservableCollection<Joueur> Joueurs => _joueurService.Joueurs;

    [ObservableProperty]
    private Joueur? selectedJoueur;

    public PagePrincipaleViewModel(JoueurService joueurService)
    {
        _joueurService = joueurService;
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
    private async Task AjouterJoueur()
    {
        var addWindow = new AddJoueurWindow();
        var vm = new AddJoueurViewModel();
        addWindow.DataContext = vm;

        var desktop = Application.Current!.ApplicationLifetime as IClassicDesktopStyleApplicationLifetime;

        if (desktop?.MainWindow is null)
            return; // sécurité

        var result = await addWindow.ShowDialog<Joueur?>(desktop.MainWindow);


        if (result != null)
        {
            _joueurService.Ajouter(result);
        }
    }

    [RelayCommand]
    private void SupprimerJoueurInline(Joueur joueurASupprimer)
    {
        if (joueurASupprimer != null)
        {
            // Si le joueur supprimé est le joueur actuellement sélectionné, désélectionnez-le.
            if (SelectedJoueur == joueurASupprimer)
            {
                SelectedJoueur = null;
            }

            _joueurService.Supprimer(joueurASupprimer); // Utilise le service pour la suppression
        }
    }
    [RelayCommand]
    private async Task VoirStats(Joueur joueur)
    {
        if (joueur == null) return;

        var fenetre = new JoueurStatsWindow(joueur);

        if (Application.Current?.ApplicationLifetime is IClassicDesktopStyleApplicationLifetime desktop)
        {
            if (desktop.MainWindow is not null)
            {
                await fenetre.ShowDialog(desktop.MainWindow);
            }
        }
    }
}