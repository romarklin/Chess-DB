using Chess_DB.Models;
using System.Collections.ObjectModel;
using System.Collections.Generic;
using System;
using System.IO;
using System.Linq;
using System.Text.Json;

namespace Chess_DB.Services;

public class CompetitionService
{

    private const string FilePath = "competitions.json";

    // La liste des parties de la compétition en cours
    public ObservableCollection<Partie> Parties { get; } = new();

    public ObservableCollection<Competition> Competitions { get; } = new();

    // La compétition active
    public Competition? CompetitionEnCours { get; private set; }

    public CompetitionService()
    {
        // IMPORTANT : Charger les données existantes au démarrage
        Charger();
    }
    public void DemarrerNouvelleCompetition(List<Joueur> joueursInscrits, string nomCompetition)
    {
        if (CompetitionEnCours != null && !Competitions.Contains(CompetitionEnCours))
        {
            CompetitionEnCours.Parties = Parties.ToList(); // Sauvegarde l'état des parties
            Competitions.Add(CompetitionEnCours);
        }

        Parties.Clear();

        // 1. Créer l'objet Compétition
        CompetitionEnCours = new Competition(1, nomCompetition, System.DateTime.Now, System.DateTime.Now.AddDays(1));

        Competitions.Add(CompetitionEnCours);

        // 2. Générer les parties (Round-Robin : Tout le monde contre tout le monde)
        int idCompteur = 1;

        for (int i = 0; i < joueursInscrits.Count; i++)
        {
            for (int j = i + 1; j < joueursInscrits.Count; j++)
            {
                var joueur1 = joueursInscrits[i];
                var joueur2 = joueursInscrits[j];
                var partie = new Partie(idCompteur++, joueur1, joueur2, CompetitionEnCours);
                Parties.Add(partie);
            }
        }

        CompetitionEnCours.Parties = Parties.ToList();

        Sauvegarder();
    }

    private List<string> ParseCoups(string coupsStr)
    {
        var list = new List<string>();
        if (string.IsNullOrWhiteSpace(coupsStr)) return list;

        // Découpe par ligne
        var lines = coupsStr.Split(new[] { '\r', '\n' }, StringSplitOptions.RemoveEmptyEntries);

        foreach (var line in lines)
        {
            // Découpe par espace ou tabulation
            var parts = line.Split(new[] { ' ', '\t', '.' }, StringSplitOptions.RemoveEmptyEntries);

            foreach (var part in parts)
            {
                // On ignore les numéros (ex: "1") et les placeholders ("...")
                if (!int.TryParse(part, out _) && part != "...")
                {
                    list.Add(part);
                }
            }
        }
        return list;
    }

    public void Sauvegarder()
    {
        // Mise à jour des parties de la compétition en cours avant sauvegarde
        if (CompetitionEnCours != null)
        {
            CompetitionEnCours.Parties = Parties.ToList();
        }

        // Transformation des données vers le format DTO (Data Transfer Object) souhaité
        var competitionsDto = Competitions.Select(c => new CompetitionDto
        {
            Nom = c.Nom,
            Date = c.DateDebut,
            Parties = c.Parties.Select(p => new PartieDto
            {
                Blancs = $"{p.JoueurBlanc.Prenom} {p.JoueurBlanc.Nom}".Trim(),
                Noirs = $"{p.JoueurNoir.Prenom} {p.JoueurNoir.Nom}".Trim(),
                Resultat = p.Resultat,
                Coups = ParseCoups(p.Coups)
            }).ToList()
        }).ToList();

        // Configuration pour le format JSON (indentation + camelCase)
        var options = new JsonSerializerOptions
        {
            WriteIndented = true,
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase
        };

        var json = JsonSerializer.Serialize(competitionsDto, options);
        File.WriteAllText(FilePath, json);
    }

    public void Charger()
    {
        if (!File.Exists(FilePath)) return;

        try
        {
            string json = File.ReadAllText(FilePath);
            var options = new JsonSerializerOptions { PropertyNamingPolicy = JsonNamingPolicy.CamelCase };
            var dtos = JsonSerializer.Deserialize<List<CompetitionDto>>(json, options);

            if (dtos != null)
            {
                Competitions.Clear();
                foreach (var dto in dtos)
                {
                    // 1. Reconstruire la compétition
                    var comp = new Competition(0, dto.Nom, dto.Date, dto.Date);

                    int idPartie = 1;
                    foreach (var pDto in dto.Parties)
                    {
                        // 2. Reconstruire des Joueurs "temporaires" pour l'historique
                        // (On essaie de séparer Prénom/Nom basiquement)
                        var blanc = ReconstructJoueur(pDto.Blancs);
                        var noir = ReconstructJoueur(pDto.Noirs);

                        var partie = new Partie(idPartie++, blanc, noir, comp, pDto.Resultat);

                        // 3. Reconstruire le texte des coups depuis la liste JSON
                        partie.Coups = ReconstructCoupsText(pDto.Coups);

                        comp.Parties.Add(partie);
                    }
                    Competitions.Add(comp);
                }
            }
        }
        catch (Exception)
        {
            // Gestion d'erreur silencieuse ou log si le fichier est corrompu
        }
    }

    private string ReconstructCoupsText(List<string> coupsList)
    {
        if (coupsList == null || coupsList.Count == 0) return "";

        System.Text.StringBuilder sb = new();
        for (int i = 0; i < coupsList.Count; i += 2)
        {
            string blanc = coupsList[i];
            string noir = (i + 1 < coupsList.Count) ? coupsList[i + 1] : "";
            sb.AppendLine($"{(i / 2) + 1}. {blanc} {noir}");
        }
        return sb.ToString();
    }

    private Joueur ReconstructJoueur(string fullName)
    {
        var parts = fullName.Split(' ', 2);
        string prenom = parts.Length > 1 ? parts[0] : "";
        string nom = parts.Length > 1 ? parts[1] : parts[0];
        return new Joueur { Prenom = prenom, Nom = nom };
    }

    public void SupprimerCompetition(Competition competition)
    {
        if (Competitions.Contains(competition))
        {
            Competitions.Remove(competition);
            Sauvegarder();
        }
    }
}

public class CompetitionDto
{
    public string Nom { get; set; } = "";
    public DateTime Date { get; set; }
    public List<PartieDto> Parties { get; set; } = new();
}
public class PartieDto
{
    public string Blancs { get; set; } = "";
    public string Noirs { get; set; } = "";
    public string Resultat { get; set; } = "";
    public List<string> Coups { get; set; } = new();
}