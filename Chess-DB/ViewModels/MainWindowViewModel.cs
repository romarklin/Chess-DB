using Chess_DB.Models;
using Chess_DB.Services;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace Chess_DB.ViewModels;

public partial class MainWindowViewModel : ObservableObject
{

    private readonly JoueurService _joueurService;

    private readonly CompetitionService _sharedCompetitionService;

    [ObservableProperty]
    private ViewModelBase _pageActuelle;

    public PagePrincipaleViewModel PagePrincipale { get; }
    public InscriptionsViewModel PageInscriptions { get; }
    public CompetitionsViewModel PageCompetitions { get; }

    public HistoriqueViewModel PageHistorique { get; }

    public MainWindowViewModel()
    {
        _joueurService = new JoueurService();
        _sharedCompetitionService = new CompetitionService();

        PagePrincipale = new PagePrincipaleViewModel(_joueurService);
        PageCompetitions = new CompetitionsViewModel(_sharedCompetitionService, () =>
        {
            PageActuelle = PageHistorique;
        });
        PageHistorique = new HistoriqueViewModel(_sharedCompetitionService, (competition) =>
        {
            PageActuelle = new DetailsCompetitionViewModel(competition, () =>
            {
                PageActuelle = PageHistorique;
            });
        });
        PageInscriptions = new InscriptionsViewModel(_joueurService, _sharedCompetitionService, () =>
        {
            PageActuelle = PageCompetitions;
        });

        PageActuelle = PagePrincipale;
    }

    [RelayCommand]
    private void AllerAlaPagePrincipale()
    {
        PageActuelle = PagePrincipale;
    }

    [RelayCommand]
    private void AllerALaPageInscriptions()
    {
        PageActuelle = PageInscriptions;
    }

    [RelayCommand]
    private void AllerALaPageCompetitions()
    {
        PageActuelle = PageHistorique;
    }
}