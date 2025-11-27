using Chess_DB.Models;
using Chess_DB.Services;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System;

namespace Chess_DB.ViewModels;

public partial class EncoderResultatViewModel : ViewModelBase
{
    public Partie PartieEnCours { get; }

    // Le texte des coups saisi par l'utilisateur
    [ObservableProperty]
    private string texteCoups;

    // Le résultat sélectionné (0 = Pas choisi, 1 = 1-0, 2 = 0-1, 3 = Nul)
    [ObservableProperty]
    private int selectedResultIndex = -1;

    // Action pour fermer la fenêtre
    public Action? CloseAction { get; set; }

    public EncoderResultatViewModel(Partie partie)
    {
        PartieEnCours = partie;
        TexteCoups = partie.Coups; // Charger les coups existants si on édite

        // Pré-sélectionner le résultat si déjà existant
        if (partie.Resultat == "1-0") SelectedResultIndex = 0;
        else if (partie.Resultat == "0-1") SelectedResultIndex = 1;
        else if (partie.Resultat == "1/2-1/2") SelectedResultIndex = 2;
    }

    [RelayCommand]
    private void Valider()
    {
        string resultatStr = "";
        switch (SelectedResultIndex)
        {
            case 0: resultatStr = "1-0"; break;
            case 1: resultatStr = "0-1"; break;
            case 2: resultatStr = "1/2-1/2"; break;
            default: return; // Rien n'est sélectionné
        }

        // 1. Sauvegarder les données dans la partie
        PartieEnCours.Coups = TexteCoups;
        PartieEnCours.Resultat = resultatStr;

        // 2. Calculer les nouveaux ELO
        EloCalculator.UpdateElo(PartieEnCours.JoueurBlanc, PartieEnCours.JoueurNoir, resultatStr);

        // 3. Fermer la fenêtre
        CloseAction?.Invoke();
    }
}