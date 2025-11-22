using Chess_DB.Models;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace Chess_DB.ViewModels;

public partial class MainWindowViewModel : ObservableObject
{

    [ObservableProperty]
    private ViewModelBase _pageActuelle;

    private readonly PagePrincipaleViewModel _pagePrincipale = new();
    private readonly InscriptionsViewModel _pageInscriptions = new();
    private readonly CompetitionsViewModel _pageCompetitions = new();

    public MainWindowViewModel()
    {
        // Initialisation du service
        _pageActuelle = _pagePrincipale;
    }

    [RelayCommand]
    private void AllerAlaPagePrincipale()
    {
        PageActuelle = _pagePrincipale;
    }

    [RelayCommand]
    private void AllerALaPageInscriptions()
    {
        PageActuelle = _pageInscriptions;
    }

    [RelayCommand]
    private void AllerALaPageCompetitions()
    {
        PageActuelle = _pageCompetitions;
    }
}