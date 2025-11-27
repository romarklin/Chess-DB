using Chess_DB.Models;
using Chess_DB.Services;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System;
using System.Collections.ObjectModel;
using System.Linq;

namespace Chess_DB.ViewModels;

public partial class EncoderResultatViewModel : ViewModelBase
{
    public Partie PartieEnCours { get; }
    public ObservableCollection<CoupItem> ListeCoups { get; } = new();

    [ObservableProperty] private string inputCoupBlanc = "";
    [ObservableProperty] private string inputCoupNoir = "";

    // Le résultat sélectionné (0 = Pas choisi, 1 = 1-0, 2 = 0-1, 3 = Nul)
    [ObservableProperty]
    private int selectedResultIndex = -1;

    public Action? CloseAction { get; set; }

    public EncoderResultatViewModel(Partie partie)
    {
        PartieEnCours = partie;

        // Pré-sélectionner le résultat si déjà existant
        if (partie.Resultat == "1-0") SelectedResultIndex = 0;
        else if (partie.Resultat == "0-1") SelectedResultIndex = 1;
        else if (partie.Resultat == "1/2-1/2") SelectedResultIndex = 2;
    }

    [RelayCommand]
    private void AjouterCoup()
    {
        if (string.IsNullOrWhiteSpace(InputCoupBlanc)) return;

        ListeCoups.Add(new CoupItem
        {
            Numero = ListeCoups.Count + 1,
            Blanc = InputCoupBlanc,
            Noir = string.IsNullOrWhiteSpace(InputCoupNoir) ? "" : InputCoupNoir
        });

        InputCoupBlanc = "";
        InputCoupNoir = "";
    }

    [RelayCommand]
    private void SupprimerDernierCoup()
    {
        if (ListeCoups.Any())
        {
            ListeCoups.Remove(ListeCoups.Last());
        }
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
            default: return;
        }

        // 1. Sauvegarde du résultat
        PartieEnCours.Resultat = resultatStr;

        // Sauvegarde des coups
        System.Text.StringBuilder sb = new();
        foreach (var item in ListeCoups)
        {
            // Format standard : "1. e4 e5"
            // On vérifie que les coups ne sont pas vides
            string coupBlanc = string.IsNullOrWhiteSpace(item.Blanc) ? "" : item.Blanc;
            string coupNoir = string.IsNullOrWhiteSpace(item.Noir) ? "" : item.Noir;

            sb.AppendLine($"{item.Numero}. {coupBlanc} {coupNoir}");
        }
        PartieEnCours.Coups = sb.ToString();

        // 2. Calcul du nouvel ELO
        EloCalculator.UpdateElo(PartieEnCours.JoueurBlanc, PartieEnCours.JoueurNoir, resultatStr);

        CloseAction?.Invoke();
    }

    public class CoupItem
    {
        public int Numero { get; set; }
        public string Blanc { get; set; } = "";
        public string Noir { get; set; } = "";

        // Pour l'affichage facile
        public string Affichage => $"{Numero}. {Blanc}   {Noir}";
    }
}