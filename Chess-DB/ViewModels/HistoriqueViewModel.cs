using Chess_DB.Models;
using Chess_DB.Services;
using System;
using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.Input;

namespace Chess_DB.ViewModels;

public partial class HistoriqueViewModel : ViewModelBase
{
    private readonly CompetitionService _competitionService;
    private readonly Action<Competition> _voirDetailsNavigation;

    public string Title { get; } = "Historique des compétitions";

    // On lie directement la liste du service
    public ObservableCollection<Competition> Competitions => _competitionService.Competitions;

    public HistoriqueViewModel(CompetitionService competitionService, Action<Competition> voirDetailsNavigation)
    {
        _competitionService = competitionService;
        _voirDetailsNavigation = voirDetailsNavigation;
    }

    [RelayCommand]
    private void VoirDetails(Competition competition)
    {
        if (competition != null)
        {
            _voirDetailsNavigation?.Invoke(competition);
        }
    }

    [RelayCommand]
    private void Supprimer(Competition competition)
    {
        if (competition != null)
        {
            _competitionService.SupprimerCompetition(competition);
        }
    }
}