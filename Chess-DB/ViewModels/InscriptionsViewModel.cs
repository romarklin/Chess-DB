using Chess_DB.Models;
using Chess_DB.Services;
using System.Collections.ObjectModel;

namespace Chess_DB.ViewModels;

public partial class InscriptionsViewModel : ViewModelBase
{
    private readonly JoueurService _joueurService;

    public ObservableCollection<Joueur> Joueurs => _joueurService.Joueurs;

    public InscriptionsViewModel(JoueurService joueurService)
    {
        _joueurService = joueurService;
    }

}