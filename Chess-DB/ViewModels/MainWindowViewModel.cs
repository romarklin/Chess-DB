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

    public MainWindowViewModel()
    {
        _joueurService = new JoueurService();
        _sharedCompetitionService = new CompetitionService();

        PagePrincipale = new PagePrincipaleViewModel(_joueurService);
        /*PageInscriptions = new InscriptionsViewModel(_joueurService);
        PageCompetitions = new CompetitionsViewModel();*/

        PageInscriptions = new InscriptionsViewModel(_joueurService, _sharedCompetitionService, () =>
        {
            PageActuelle = PageCompetitions; // L'action qui change la page
        });

        // MODIFICATION : On passe le service aux compétitions pour qu'il puisse lire la liste
        PageCompetitions = new CompetitionsViewModel(_sharedCompetitionService);

        _pageActuelle = PagePrincipale;
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
        PageActuelle = PageCompetitions;
    }
}