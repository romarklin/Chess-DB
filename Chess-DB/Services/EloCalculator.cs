using System;

namespace Chess_DB.Services;

public static class EloCalculator
{
    // K-Factor : détermine la volatilité du classement. 32 est standard pour les amateurs/clubs.
    private const int K = 32;

    public static void UpdateElo(Models.Joueur blanc, Models.Joueur noir, string resultat)
    {
        double scoreBlanc, scoreNoir;

        switch (resultat)
        {
            case "1-0":
                scoreBlanc = 1.0;
                scoreNoir = 0.0;
                break;
            case "0-1":
                scoreBlanc = 0.0;
                scoreNoir = 1.0;
                break;
            default: // "1/2-1/2" ou autre
                scoreBlanc = 0.5;
                scoreNoir = 0.5;
                break;
        }

        // Calcul de l'espérance de gain (Expected Score)
        // Formule : E = 1 / (1 + 10 ^ ((EloAdverse - EloJoueur) / 400))
        double esperanceBlanc = 1.0 / (1.0 + Math.Pow(10, (noir.ClassementElo - blanc.ClassementElo) / 400.0));
        double esperanceNoir = 1.0 / (1.0 + Math.Pow(10, (blanc.ClassementElo - noir.ClassementElo) / 400.0));

        // Nouveau ELO = Ancien ELO + K * (ScoreRéel - Espérance)
        int nouveauEloBlanc = (int)Math.Round(blanc.ClassementElo + K * (scoreBlanc - esperanceBlanc));
        int nouveauEloNoir = (int)Math.Round(noir.ClassementElo + K * (scoreNoir - esperanceNoir));

        // Mise à jour des joueurs
        blanc.ClassementElo = nouveauEloBlanc;
        noir.ClassementElo = nouveauEloNoir;

        // Mise à jour des stats
        blanc.PartiesJouees++;
        noir.PartiesJouees++;

        if (scoreBlanc == 1) { blanc.Victoires++; noir.Defaites++; }
        else if (scoreNoir == 1) { noir.Victoires++; blanc.Defaites++; }
        else { blanc.Nuls++; noir.Nuls++; }
    }
}