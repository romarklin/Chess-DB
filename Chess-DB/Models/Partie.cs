using System;
using System.Collections.Generic;
using System.ComponentModel; // Pour List<T>

namespace Chess_DB.Models

{
    public class Partie
    {
        public int Id { get; set; }

        // Joueurs de la partie
        public Joueur JoueurBlanc { get; set; }
        public Joueur JoueurNoir { get; set; }

        // Résultat de la partie : "1-0", "0-1", "½-½"
        public string Resultat { get; set; } = string.Empty;

        // Compétition à laquelle la partie appartient
        public Competition Competition { get; set; }

        // Date de la partie
        public DateTime Date { get; set; } = DateTime.Now;

        //public Partie() { }

        public Partie(int id, Joueur blanc, Joueur noir, Competition competition, string resultat = "")
        {
            Id = id;
            JoueurBlanc = blanc;
            JoueurNoir = noir;
            Competition = competition;
            Resultat = resultat;
        }

        public override string ToString()
        {
            return $"{JoueurBlanc} vs {JoueurNoir} - {Resultat}";
        }
    }
}