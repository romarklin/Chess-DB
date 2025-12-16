using CommunityToolkit.Mvvm.ComponentModel;
using System;

namespace Chess_DB.Models;

public partial class Partie : ObservableObject
{
    public int Id { get; set; }

    public Joueur JoueurBlanc { get; set; }
    public Joueur JoueurNoir { get; set; }

    [ObservableProperty]
    [NotifyPropertyChangedFor(nameof(PeutEncoder))]
    private string resultat = string.Empty;

    public bool PeutEncoder => string.IsNullOrEmpty(Resultat);

    [ObservableProperty]
    private string coups = string.Empty;

    public Competition Competition { get; set; }
    public DateTime Date { get; set; } = DateTime.Now;

    public Partie(int id, Joueur blanc, Joueur noir, Competition competition, string resultat = "")
    {
        Id = id;
        JoueurBlanc = blanc;
        JoueurNoir = noir;
        Competition = competition;
        Resultat = resultat;
    }

    public override string ToString() => $"{JoueurBlanc} vs {JoueurNoir} - {Resultat}";
}