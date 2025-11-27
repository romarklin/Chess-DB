using Chess_DB.Models;
using System.Collections.ObjectModel;
using System.Collections.Generic;

namespace Chess_DB.Services;

public class CompetitionService
{
    // La liste des parties de la compétition en cours
    public ObservableCollection<Partie> Parties { get; } = new();

    // La compétition active
    public Competition? CompetitionEnCours { get; private set; }

    public void DemarrerNouvelleCompetition(List<Joueur> joueursInscrits, string nomCompetition)
    {
        Parties.Clear();

        // 1. Créer l'objet Compétition
        CompetitionEnCours = new Competition(1, nomCompetition, System.DateTime.Now, System.DateTime.Now.AddDays(1));

        // 2. Générer les parties (Round-Robin : Tout le monde contre tout le monde)
        int idCompteur = 1;

        for (int i = 0; i < joueursInscrits.Count; i++)
        {
            for (int j = i + 1; j < joueursInscrits.Count; j++)
            {
                var joueur1 = joueursInscrits[i];
                var joueur2 = joueursInscrits[j];

                // Création de la partie
                var partie = new Partie(idCompteur++, joueur1, joueur2, CompetitionEnCours);
                Parties.Add(partie);
            }
        }
    }
}