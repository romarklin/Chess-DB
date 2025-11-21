using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Chess_DB.Models;
using System;

namespace Chess_DB.ViewModels;

public partial class AddJoueurViewModel : ObservableObject
{
    [ObservableProperty] private string nom = "";
    [ObservableProperty] private string prenom = "";

    public Joueur? CreatedJoueur { get; private set; }

    public event Action? CloseRequested;

    [RelayCommand]
    private void Annuler()
    {
        CreatedJoueur = null;
        CloseRequested?.Invoke();
    }

    [RelayCommand]
    private void Confirmer()
    {
        if (string.IsNullOrWhiteSpace(Nom) || string.IsNullOrWhiteSpace(Prenom))
            return;

        CreatedJoueur = new Joueur(0, Nom, Prenom, "");
        CloseRequested?.Invoke();
    }
}