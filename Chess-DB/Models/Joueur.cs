using System.Text.Json.Serialization;
using CommunityToolkit.Mvvm.ComponentModel;

namespace Chess_DB.Models;

public partial class Joueur : ObservableObject
{

    [ObservableProperty]
    private string nom = string.Empty;

    [ObservableProperty]
    private string prenom = string.Empty;

    [ObservableProperty]
    private double classementElo = 1200;

    [ObservableProperty]
    private int partiesJouees;

    [ObservableProperty]
    private int victoires;

    [ObservableProperty]
    private int defaites;

    [ObservableProperty]
    private int nuls;

    // Constructeur par défaut
    public Joueur() { }

    // Constructeur pratique pour initialiser rapidement un joueur
    public Joueur(string nom, string prenom, double classementElo = 1200)
    {
        Nom = nom;
        Prenom = prenom;
        ClassementElo = classementElo;
    }

    // Méthode utilitaire pour calculer le taux de victoire
    public double TauxDeVictoire()
    {
        if (PartiesJouees == 0) return 0;
        return (double)Victoires / PartiesJouees * 100;
    }

    // Redéfinition de ToString()
    public override string ToString()
    {
        return $"{Prenom} {Nom} (ELO: {ClassementElo})";
    }
}