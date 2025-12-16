using Chess_DB.Models;
using Chess_DB.Services;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace Chess_DB.ViewModels;

public partial class MainWindowViewModel : ViewModelBase
{
    [ObservableProperty]
    private ViewModelBase _pageActuelle;

    private readonly PagePrincipaleViewModel _pagePrincipale;
    private readonly InscriptionsViewModel _pageInscriptions;
    private readonly CompetitionsViewModel _pageCompetitions;
    private readonly HistoriqueViewModel _pageHistorique;

    private readonly JoueurService _sharedJoueurService;
    private readonly CompetitionService _sharedCompetitionService;

    // Un indicateur pour savoir si un tournoi est actif
    private bool _competitionEnCours = false;

    public MainWindowViewModel()
    {
        _sharedJoueurService = new JoueurService();
        _sharedCompetitionService = new CompetitionService();

        _pagePrincipale = new PagePrincipaleViewModel(_sharedJoueurService);

        // 1. Quand on démarre une compétition, on appelle 'AllerVersCompetitions'
        _pageInscriptions = new InscriptionsViewModel(
            _sharedJoueurService,
            _sharedCompetitionService,
            AllerVersCompetitions
        );

        // 2. Quand on termine une compétition, on appelle 'TerminerCompetition'
        _pageCompetitions = new CompetitionsViewModel(
            _sharedCompetitionService,
            TerminerCompetition
        );

        _pageHistorique = new HistoriqueViewModel(
            _sharedCompetitionService,
            AllerVersDetails
        );

        _pageActuelle = _pagePrincipale;
    }

    // --- Méthodes de navigation ---

    private void AllerVersCompetitions()
    {
        // On marque le début du tournoi
        _competitionEnCours = true;
        PageActuelle = _pageCompetitions;
    }

    private void TerminerCompetition()
    {
        // On marque la fin du tournoi
        _competitionEnCours = false;
        PageActuelle = _pageHistorique;
    }

    private void AllerVersHistorique()
    {
        // Navigation simple (utilisée par le bouton retour des détails)
        PageActuelle = _pageHistorique;
    }

    private void AllerVersDetails(Competition competition)
    {
        PageActuelle = new DetailsCompetitionViewModel(competition, AllerVersHistorique);
    }

    // --- Commandes du menu ---

    [RelayCommand]
    private void AllerAlaPagePrincipale()
    {
        PageActuelle = _pagePrincipale;
    }

    [RelayCommand]
    private void AllerALaPageInscriptions()
    {
        if (_competitionEnCours)
        {
            // Si une compétition est active, ce bouton sert de raccourci pour y revenir
            PageActuelle = _pageCompetitions;
        }
        else
        {
            // Sinon, il mène normalement à la page d'inscription
            PageActuelle = _pageInscriptions;
        }
    }

    [RelayCommand]
    private void AllerALaPageCompetitions()
    {
        PageActuelle = _pageCompetitions;
    }

    [RelayCommand]
    private void AllerALaPageHistorique()
    {
        PageActuelle = _pageHistorique;
    }
}