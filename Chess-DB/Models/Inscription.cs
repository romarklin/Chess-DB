using System;
using System.Collections.Generic; // Pour List<T>

namespace Chess_DB.Models

{
    public class Inscription
    {
        public int Id { get; set; }

        // Joueur inscrit
        public Joueur Joueur { get; set; }

        // Compétition concernée
        public Competition Competition { get; set; }

        // Date d'inscription
        public DateTime DateInscription { get; set; } = DateTime.Now;

        //public Inscription() { }

        public Inscription(int id, Joueur joueur, Competition competition)
        {
            Id = id;
            Joueur = joueur;
            Competition = competition;
            DateInscription = DateTime.Now;
        }

        public override string ToString()
        {
            return $"{Joueur} inscrit à {Competition}";
        }
    }
}
